using LexHarvester.Application.Steps.LegislationTypeSync;
using Navend.Core.Step.Abstract;

namespace LexHarvester.Application.Seeding;

public class LegislationTypeSync : ITableSync
{
    public int Order => 1001;
    private readonly List<IStep<LegislationTypeStepContext>> _steps;
    public LegislationTypeSync(IEnumerable<IStep<LegislationTypeStepContext>> steps)
    {
        _steps = steps.OrderBy(s=>s.Order).ToList();
    }

    public async Task SyncAsync(CancellationToken cancellationToken = default)
    {
        foreach (var step in _steps)
            await step.ExecuteAsync();
    }
}