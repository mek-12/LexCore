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
    private readonly IRepository<HarvestingState, int> _harvestingRepository = unitOfWork.GetRepository<HarvestingState, int>();
    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var legislationTypes = context.LegislationTypes.OrderBy(l => l.LegislationTypeCode);
        HarvestingState? currentHarvestingState = null;
        try
        {
            foreach (var legislationType in legislationTypes)
            {
                currentHarvestingState = context.HarvestingStates.FirstOrDefault(h => h.SubType == legislationType.LegislationTypeCode);
                if (currentHarvestingState == null)
                {
                    currentHarvestingState = new HarvestingState { SubType = legislationType.LegislationTypeCode, DocumentType = DocumentType.Legislation };
                    context.HarvestingStates.Add(currentHarvestingState);
                }
                if (currentHarvestingState.Count >= legislationType.Count) continue;

                await UpdateDocumentReferencesFromSource(currentHarvestingState, legislationType.Count);
                currentHarvestingState.CurrentPage--;
            }
        }
        catch (Exception ex)
        {
            if (currentHarvestingState != null)
            {
                currentHarvestingState.LastErrorMessage = ex.ToString();
            }
            // TO DO: Find usable logging mechanism for container platforms
        }
        finally
        {
            await _harvestingRepository.UpdateRangeAsync(context.HarvestingStates);
        }
    }

    private async Task UpdateDocumentReferencesFromSource(HarvestingState state, int legislationCount)
    {
        var totalPage = Math.Ceiling((decimal)((double)legislationCount / Constants.PAGE_SIZE));
        var _legislations = new List<LegislationDocumentReference>();

        for (int i = state.CurrentPage; i <= totalPage; i = state.CurrentPage)
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
            _legislations.AddRange(legislations);
        }
        await _repository.AddRangeAsync(_legislations);
    }

    private async Task<LegislationDocumentReferenceResponse> GetLegislationDocumentReferenceAsync(HarvestingState state)
    {
        if (string.IsNullOrEmpty(state.SubType))
        {
            throw new ArgumentNullException(nameof(state.SubType));
        }
        var request = new LegislationDocumentReferenceRequest
        {
            Data = new LegislationRequestData
            {
                MevzuatTurList = new List<string> { state.SubType },
                SortFields = new List<string> { Constants.RESMI_GAZETE_TARIHI },
                PageNumber = state.CurrentPage++
            }
        };
        return await documentReferenceProvider.SendAsync(request);
    }
}