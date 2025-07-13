using LexHarvester.Application.Steps.LegislationDocumentReferenceSync;
using Navend.Core.Step.Abstract;

namespace LexHarvester.Application.Seeding.Concrete;

public class LegislationDocumentReferenceSync(IEnumerable<IStep<LegislationDocumentReferenceStepContext>> steps) : ITableSync
{
    public int Order => 2;

    public async Task SyncAsync(CancellationToken cancellationToken = default)
    {
        foreach (var step in steps.OrderBy(s => s.Order))
        {
            await step.ExecuteAsync(); 
        }
    }
}
