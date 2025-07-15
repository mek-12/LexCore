using LexHarvester.Domain.DTOs;
using LexHarvester.Infrastructure.Cache.Abstract;
using Microsoft.Extensions.Logging;

namespace LexHarvester.Application.Seeding;

public class TableSyncOrchestrator(IEnumerable<ITableSync> seeders,
                                   ILogger<TableSyncOrchestrator> logger,
                                   ISyncConfigurationCache syncConfiguration) : ITableSyncOrchestrator
{
    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        foreach (var seeder in seeders)
        {
            try
            {
                var isActive = syncConfiguration.TryGet(seeder.Name, out SyncConfigurationDto? conf) ? conf?.IsActive ?? true : true;
                if (!isActive) continue;

                var seederType = seeder.GetType().Name;
                logger.LogInformation("Seeder started: {Seeder}", seederType);

                await seeder.SyncAsync(cancellationToken);

                logger.LogInformation("Seeder finished: {Seeder}", seederType);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Seeder failed: {Seeder}", seeder.GetType().Name);
            }
        }
    }
}
