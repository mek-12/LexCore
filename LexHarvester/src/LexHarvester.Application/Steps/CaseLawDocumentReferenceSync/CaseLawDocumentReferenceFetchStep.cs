using AutoMapper;
using LexHarvester.Application.Extensions;
using LexHarvester.Domain.Const;
using LexHarvester.Domain.DTOs;
using LexHarvester.Domain.Entities;
using LexHarvester.Domain.Enums;
using LexHarvester.Infrastructure.Providers.Abstract;
using LexHarvester.Infrastructure.Providers.Respose;
using Navend.Core.Step.Abstract;

namespace LexHarvester.Application.Steps.CaseLawDocumentReferenceSync;

public class CaseLawDocumentReferenceFetchStep(IMapper mapper,
                                                CaseLawDocumentReferenceContext context,
                                                ICaseLawDocumentReferenceProvider caseLawDocumentReferenceProvider) : IStep<CaseLawDocumentReferenceContext>
{
    public int Order => 1;
    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var semaphore = new SemaphoreSlim(20); // aynı anda 20 işlem sınırı
        var tasks = new List<Task>();

        foreach (var caseLawType in context.CaseLawTypes)
        {
            var divisions = context.CaseLawDivisions.Where(d => d.ItemType == caseLawType.Name);
            foreach (var division in divisions)
            {
                // İşlemi bir yardımcı methoda sar: Task.Run kullanmadan
                tasks.Add(RunWithSemaphoreAsync(() =>
                    UpdateDocumentReferences(caseLawType, division),
                    semaphore,
                    cancellationToken));
            }
        }

        await Task.WhenAll(tasks); // Hepsini bekle
    }

    private async Task RunWithSemaphoreAsync(Func<Task> taskFunc, SemaphoreSlim semaphore, CancellationToken ct)
    {
        await semaphore.WaitAsync(ct);
        try
        {
            await taskFunc();
        }
        finally
        {
            semaphore.Release();
        }
    }

    private async Task UpdateDocumentReferences(CaseLawType caseLawType, CaseLawDivision division)
    {
        var stateSubType = $"{caseLawType.Name}_{division.Name}";
        var state = context.HarvestingStates.Get(DocumentType.CaseLaw, stateSubType);

        // TO DO: Burada state get yaptıktan sonra Count 0 ise ilk response dan Count bilgisini alıp yazmak lazım.

        var divideRemainder = Math.Ceiling((decimal)((double)state.Count / Constants.PAGE_SIZE_100));
        var isLastPageFull = divideRemainder == 0;
        var startPage = isLastPageFull ? state.CurrentPage + 1 : state.CurrentPage;
        var isCompleted = false;
        List<CaseLawDocumentReferenceDto> documents = new();
        for (int i = startPage; !isCompleted; startPage++)
        {
            CaseLawDocumentReferenceResponse response = await caseLawDocumentReferenceProvider.SendAsync(new()
            {
                Data = new()
                {
                    ItemTypeList = new() { caseLawType.Name },
                    DivisionName = division.Name,
                    SortFields = new() { Constants.KARAR_TARIHI },
                    PageSize = Constants.PAGE_SIZE_100,
                    PageNumber = startPage
                }
            });
            if (response.Data?.ListOfPrecedentDecisions?.Count != Constants.PAGE_SIZE_100 && response.Data?.ListOfPrecedentDecisions?.Count > 0)
            {
                state.Count = (startPage - 1) * Constants.PAGE_SIZE_100 + response.Data?.ListOfPrecedentDecisions?.Count ?? 0;
                state.CurrentPage = startPage;
                isCompleted = state.IsCompleted = true;
            }
            if (response.Data?.ListOfPrecedentDecisions?.Count == 0)
            {
                state.Count = (startPage - 1) * Constants.PAGE_SIZE_100;
                state.CurrentPage = startPage - 1;
                isCompleted = state.IsCompleted = true;
                continue;
            }

            if (response?.Data?.ListOfPrecedentDecisions != null)
            {
                documents.AddRange(response.Data.ListOfPrecedentDecisions);
            }
        }

        var caseLawDocuments = mapper.Map<List<CaseLawDocumentReference>>(documents.Where(d => d.DocumentId != null && !context.CaseLawDocumentIds.Contains(d.DocumentId)));
        if (caseLawDocuments.Any())
        {
            foreach (var item in caseLawDocuments)
            {
                context.CaseLawDocumentReferences.Add(item);
            }
        }
    }
}