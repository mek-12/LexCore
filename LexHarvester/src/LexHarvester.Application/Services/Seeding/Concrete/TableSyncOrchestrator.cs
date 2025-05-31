using Microsoft.Extensions.Logging;

namespace LexHarvester.Application.Services.Seeding;

public class TableSyncOrchestrator : ITableSyncOrchestrator
{
    private readonly IEnumerable<ITableSync> _seeders;
    private readonly ILogger<TableSyncOrchestrator> _logger;

    public TableSyncOrchestrator(IEnumerable<ITableSync> seeders, ILogger<TableSyncOrchestrator> logger)
    {
        _seeders = seeders;
        _logger = logger;
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        foreach (var seeder in _seeders)
        {
            try
            {
                var seederType = seeder.GetType().Name;
                _logger.LogInformation("Seeder started: {Seeder}", seederType);

                await seeder.SyncAsync(cancellationToken);

                _logger.LogInformation("Seeder finished: {Seeder}", seederType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Seeder failed: {Seeder}", seeder.GetType().Name);
            }
        }
    }
}
