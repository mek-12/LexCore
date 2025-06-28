
namespace LexHarvester.Application.Seeding;

public interface ITableSyncOrchestrator
{
    Task RunAsync(CancellationToken cancellationToken = default);
}
