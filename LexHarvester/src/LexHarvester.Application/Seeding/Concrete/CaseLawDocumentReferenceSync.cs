using LexHarvester.Application.Steps.CaseLawDocumentReferenceSync;
using Navend.Core.Step.Abstract;

namespace LexHarvester.Application.Seeding.Concrete;

public class CaseLawDocumentReferenceSync(IEnumerable<IStep<CaseLawDocumentReferenceContext>> steps) : ITableSync
{
    public int Order => 2001;

    public async Task SyncAsync(CancellationToken cancellationToken = default)
    {
        foreach (var step in steps.OrderBy(o => o.Order))
        {
            await step.ExecuteAsync();
        }
    }
}
