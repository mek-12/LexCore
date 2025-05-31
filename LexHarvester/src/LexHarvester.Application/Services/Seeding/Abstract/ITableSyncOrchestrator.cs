
namespace LexHarvester.Application.Services.Seeding;

public interface ITableSyncOrchestrator
{
    Task RunAsync(CancellationToken cancellationToken = default);
}
