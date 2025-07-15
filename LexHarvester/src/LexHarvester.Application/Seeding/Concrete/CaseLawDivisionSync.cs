using LexHarvester.Application.Steps.CaseLawDivisionSync;
using Navend.Core.Step.Abstract;

namespace LexHarvester.Application.Seeding.Concrete;

public class CaseLawDivisionSync(IEnumerable<IStep<CaseLawDivisionContext>> steps) : ITableSync
{
    public int Order => 1002;
    public async Task SyncAsync(CancellationToken cancellationToken = default)
    {
        foreach (var step in steps.OrderBy(s => s.Order))
        {
            await step.ExecuteAsync();
        }
    }
}
