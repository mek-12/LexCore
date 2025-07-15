using LexHarvester.Domain.Entities;
using LexHarvester.Domain.Enums;
using Navend.Core.Data;
using Navend.Core.Step.Abstract;
using Navend.Core.UOW;

namespace LexHarvester.Application.Steps.CaseLawTypeSync;

public class CaseLawTypeInitStep(IUnitOfWork unitOfWork,
                                 CaseLawTypeContext context) : IStep<CaseLawTypeContext>
{
    public int Order => 0;
    private readonly IRepository<CaseLawType, int> repository = unitOfWork.GetRepository<CaseLawType, int>();
    private readonly IRepository<HarvestingState, int> harvestingRepository = unitOfWork.GetRepository<HarvestingState, int>();
    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        context.CaseLawTypes.AddRange(await repository.GetAllAsync());
        context.HarvestingStates.AddRange(await harvestingRepository.GetAllAsync(h => h.DocumentType == DocumentType.CaseLaw));
    }
}
