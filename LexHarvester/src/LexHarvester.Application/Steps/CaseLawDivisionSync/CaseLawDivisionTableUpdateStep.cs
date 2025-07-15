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
                                       .Where(d => !context.DivisionsUnitIds.Contains((d.UnitId, d.ItemType)))
                                       .ToList();

        var caseLawDivisions = mapper.Map<List<CaseLawDivision>>(filteredDivisions);

        caseLawDivisions = caseLawDivisions.Where(c => !string.IsNullOrWhiteSpace(c.ItemType) &&
                                                       !string.IsNullOrWhiteSpace(c.Name)).ToList();

        await repository.AddRangeAsync(caseLawDivisions);
    }
}
