
using LexHarvester.Application.Steps.LexDocumentSync;
using Navend.Core.Step.Abstract;

namespace LexHarvester.Application.Seeding.Concrete;

public class LegislationDocumentSync(IEnumerable<IStep<LegislationDocumentStepContext>> steps) : ITableSync
{
    public int Order => 3000;

    public async Task SyncAsync(CancellationToken cancellationToken = default)
    {
        foreach (var item in steps.OrderBy(s => s.Order))
        {
            await item.ExecuteAsync(cancellationToken);
        }
    }
}
