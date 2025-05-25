using LexHarvester.Application.Services.Seeding;
using Microsoft.Extensions.Logging;
namespace LexHarvester.Application.Jobs.Concrete
{
    public class SyncTablesJob : IJob
    {
        private readonly ILogger<SyncTablesJob> _logger;
        private readonly ITableSyncOrchestrator _tableSyncOrchestrator;

        public SyncTablesJob(ILogger<SyncTablesJob> logger , ITableSyncOrchestrator tableSyncOrchestrator)
        {
            _logger = logger;
            _tableSyncOrchestrator = tableSyncOrchestrator;
        }
        public async Task Run(JobRequest jobRequest, DateTime date)
        {
            _logger.LogInformation("Starting synchronization of tables.");

            try
            {
                await _tableSyncOrchestrator.RunAsync();
                _logger.LogInformation("Finished synchronization of tables.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during table synchronization.");
            }
        }
    }
}
