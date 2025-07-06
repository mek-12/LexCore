using LexHarvester.Domain.Entities;
using LexHarvester.Domain.Enums;
using Navend.Core.Data;
using Navend.Core.Step.Abstract;
using Navend.Core.UOW;

namespace LexHarvester.Application.Steps.LegislationTypeSync;

public class LegislationHarvestingStateUpdateStep(IUnitOfWork unitOfWork, LegislationTypeStepContext context) : IStep<LegislationTypeStepContext>
{
    public int Order => 1;
    private IRepository<HarvestingState, int> _repository = unitOfWork.GetRepository<HarvestingState, int>();

    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        if (context.LegislationTypes == null || !context.LegislationTypes.Any())
            return;
        var harvestingStates = await _repository.GetAllAsync(h => h.DocumentType == DocumentType.Legislation);

        context.LegislationTypes.ForEach(legislationType =>
        {
            var state = harvestingStates.FirstOrDefault(h => h.DocumentType == DocumentType.Legislation && h.SubType == legislationType.LegislationTypeCode);
            if (state != null)
            {
                state.Synchronized = legislationType.Count == state.Count;
            }
            else
            {
                harvestingStates.Add(
                    new HarvestingState
                    {
                        DocumentType = DocumentType.Legislation,
                        SubType = legislationType.LegislationTypeCode,
                        CurrentPage = 1,
                        Count = 0,
                        Synchronized = false,
                        IsCompleted = false,
                        LastUpdated = DateTime.Now,
                        CreatedAt = DateTime.Now
                    }
                );
            }
        });

        await _repository.UpsertRangeAsync(harvestingStates);
    }
}
