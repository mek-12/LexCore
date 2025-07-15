using LexHarvester.Domain.Entities;
using Navend.Core.Data;
using Navend.Core.Step.Abstract;
using Navend.Core.UOW;

namespace LexHarvester.Application.Steps.CaseLawDivisionSync;

public class CaseLawDivisionInitStep(IUnitOfWork unitOfWork,
                                     CaseLawDivisionContext caseLawDivisionContext) : IStep<CaseLawDivisionContext>
{
    public int Order => 0;
    private readonly IRepository<CaseLawType, int> caseLawTypeRepository = unitOfWork.GetRepository<CaseLawType, int>();
    private readonly IRepository<CaseLawDivision, int> caseLawDivisionRepository = unitOfWork.GetRepository<CaseLawDivision, int>();

    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        caseLawDivisionContext.CaseLawTypes.AddRange(await caseLawTypeRepository.SelectAsync(null, c => c.Name));
        caseLawDivisionContext.DivisionsUnitIds = (await caseLawDivisionRepository.SelectAsync(null,  d => new ValueTuple<string, string>(d.UnitId, d.ItemType))).ToHashSet();
    }
}
