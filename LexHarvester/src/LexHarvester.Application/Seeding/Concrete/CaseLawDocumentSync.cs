using LexHarvester.Application.Steps.CaseLawDocumentSync;
using Navend.Core.Step.Abstract;

namespace LexHarvester.Application.Seeding.Concrete;

public class CaseLawDocumentSync(IEnumerable<IStep<CaseLawDocumentSyncContext>> steps) : ITableSync
{
    public int Order => 4000;

    public async Task SyncAsync(CancellationToken cancellationToken = default)
    {
        foreach (var item in steps.OrderBy(s => s.Order))
        {
            await item.ExecuteAsync(cancellationToken);
        }
    }
}