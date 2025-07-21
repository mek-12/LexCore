using LexHarvester.Domain.Entities;
using LexHarvester.Domain.Enums;
using LexHarvester.Helper.Utils.ConcurrentBag;
using Navend.Core.Data;
using Navend.Core.Step.Abstract;
using Navend.Core.UOW;

namespace LexHarvester.Application.Steps.CaseLawDocumentReferenceSync;

public class CaseLawDocumentReferenceInitStep(IUnitOfWork unitOfWork,
                                             CaseLawDocumentReferenceContext caseLawDocumentReferenceContext) : IStep<CaseLawDocumentReferenceContext>
{
    public int Order => 0;
    private readonly IRepository<CaseLawType, int> _caseLawTypeRepository = unitOfWork.GetRepository<CaseLawType, int>();
    private readonly IRepository<HarvestingState, int> _harvestingStateRepository = unitOfWork.GetRepository<HarvestingState, int>();
    private readonly IRepository<CaseLawDivision, int> _caseLawDivisionRepository = unitOfWork.GetRepository<CaseLawDivision, int>();
    private readonly IRepository<CaseLawDocumentReference, long> _caseLawDocumentReference = unitOfWork.GetRepository<CaseLawDocumentReference, long>();
    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        caseLawDocumentReferenceContext.CaseLawDivisions = await _caseLawDivisionRepository.GetAllAsync();
        caseLawDocumentReferenceContext.CaseLawTypes = await _caseLawTypeRepository.GetAllAsync();
        caseLawDocumentReferenceContext.HarvestingStates.AddRange(await _harvestingStateRepository.GetAllAsync(h => h.DocumentType == DocumentType.CaseLaw));
        caseLawDocumentReferenceContext.CaseLawDocumentIds.AddRange(await _caseLawDocumentReference.SelectAsync(null, c => c.DocumentId));
    }
}
