using AutoMapper;
using LexHarvester.Domain.Const;
using LexHarvester.Domain.Entities;
using LexHarvester.Domain.Enums;
using LexHarvester.Infrastructure.Providers.Abstract;
using LexHarvester.Infrastructure.Providers.Request;
using Navend.Core.Data;
using Navend.Core.Step.Abstract;
using Navend.Core.UOW;

namespace LexHarvester.Application.Steps.LegislationDocumentReferenceSync;

public class LegislationDocumentReferencesUpdateStep(IUnitOfWork unitOfWork,
                                                     ILegislationDocumentReferenceProvider documentReferenceProvider,
                                                     LegislationDocumentReferenceStepContext context,
                                                     IMapper mapper)
                                                    : IStep<LegislationDocumentReferenceStepContext>
{
    public int Order => 1;
    private readonly IRepository<LegislationDocumentReference, long> _repository = unitOfWork.GetRepository<LegislationDocumentReference, long>();
    public Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var legislationTypes = context.LegislationTypes.OrderBy(l => l.LegislationTypeCode);

        try
        {
            foreach (var legislationType in legislationTypes)
            {
                var state = context.HarvestingStates.FirstOrDefault(h => h.SubType == legislationType.LegislationTypeCode);
                if (state == null)
                {
                    state = new HarvestingState { SubType = legislationType.LegislationTypeCode, DocumentType = DocumentType.Legislation };
                    context.HarvestingStates.Add(state);
                }
                if (state.Count == legislationType.Count) continue;


            }
        }
        catch (Exception ex)
        {

        }
        finally
        {

        }
        return Task.CompletedTask;
    }

    private async Task UpdateDocumentReferencesFromSource(HarvestingState state, int legislationCount)
    {
        var totalPage = Math.Ceiling((decimal)(legislationCount / Constants.PAGE_SIZE));
        for (int i = state.CurrentPage; i <= totalPage;)
        {
            var response = await GetLegislationDocumentReferenceAsync(state);
            if (!response.Data.Legislations.Any())
            {
                break;
            }

            var legislations = mapper.Map<List<LegislationDocumentReference>>(response.Data.Legislations);
            legislations.ForEach(legislation =>
            {
                if (!context.LegislationIds.Contains(legislation.LegislationId))
                    state.Count++;
            });
            await _repository.AddRangeAsync(legislations);
        }
    }

    private async Task<LegislationDocumentReferenceResponse> GetLegislationDocumentReferenceAsync(HarvestingState state)
    {
        return await documentReferenceProvider.SendAsync(new LegislationDocumentReferenceRequest
        {
            Data = new LegislationRequestData
            {
                MevzuatTurList = new List<string> { state.SubType ?? "" },
                SortFields = new List<string> { Constants.RESMI_GAZETE_TARIHI },
                PageNumber = state.CurrentPage++
            }
        });

    }
}