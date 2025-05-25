using Microsoft.Extensions.Logging;
namespace LexHarvester.Application.Jobs.Concrete
{
    public class SyncCaseLawTypesJob : IJob
    {
        private readonly ILogger<SyncCaseLawTypesJob> _logger;

        public SyncCaseLawTypesJob(ILogger<SyncCaseLawTypesJob> logger)
        {
            _logger = logger;
        }

        public Task Run(JobRequest jobRequest, DateTime date)
        {
            _logger.LogInformation("Starting synchronization of case law types.");

            // TODO: Add logic to sync case law types here.

            _logger.LogInformation("Finished synchronization of case law types.");
            return Task.CompletedTask;
        }
    }
}
