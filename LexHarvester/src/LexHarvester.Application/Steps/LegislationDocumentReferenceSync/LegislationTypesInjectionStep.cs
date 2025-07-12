using LexHarvester.Domain.Entities;
using LexHarvester.Domain.Enums;
using Navend.Core.Data;
using Navend.Core.Step.Abstract;
using Navend.Core.UOW;

namespace LexHarvester.Application.Steps.LegislationDocumentReferenceSync;

public class LegislationTypesInjectionStep(IUnitOfWork unitOfWork, LegislationDocumentReferenceStepContext context) : IStep<LegislationDocumentReferenceStepContext>
{
    public int Order { get => 0; }
    private readonly IRepository<LegislationType, int> _repository = unitOfWork.GetRepository<LegislationType, int>();
    private readonly IRepository<HarvestingState, int> _harvestingStateRepository = unitOfWork.GetRepository<HarvestingState, int>();
    private readonly IRepository<LegislationDocumentReference, long> _documentReferenceRepository = unitOfWork.GetRepository<LegislationDocumentReference, long>();

    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        context.LegislationTypes = await _repository.GetAllAsync();
        context.HarvestingStates = await _harvestingStateRepository.GetAllAsync(h => h.DocumentType == DocumentType.Legislation);
        context.LegislationIds = await _documentReferenceRepository.SelectAsync(null, x => x.LegislationId);
        return;
    }
}
