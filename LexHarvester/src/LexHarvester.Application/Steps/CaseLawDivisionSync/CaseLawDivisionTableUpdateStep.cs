using AutoMapper;
using LexHarvester.Domain.Entities;
using Navend.Core.Data;
using Navend.Core.Step.Abstract;
using Navend.Core.UOW;

namespace LexHarvester.Application.Steps.CaseLawDivisionSync;

public class CaseLawDivisionTableUpdateStep(IMapper mapper, 
                                            IUnitOfWork unitOfWork,
                                            CaseLawDivisionContext context) : IStep<CaseLawDivisionContext>
{
    public int Order => 2;
    private readonly IRepository<CaseLawDivision, int> repository = unitOfWork.GetRepository<CaseLawDivision, int>();
    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var filteredDivisions = context.CaseLawDivisionResponses
                                       .Where(d => !context.DivisionsUnitIds
                                                   .Any(x => x.UnitId == d.UnitId &&
                                                             x.ItemType == d.ItemType));
        var caseLawDivisions = mapper.Map<List<CaseLawDivision>>(filteredDivisions);
        await repository.AddRangeAsync(caseLawDivisions);
    }
}
