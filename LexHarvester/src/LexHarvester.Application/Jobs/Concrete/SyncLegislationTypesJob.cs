using Microsoft.Extensions.Logging;

namespace LexHarvester.Application.Jobs.Concrete;

public class SyncLegislationTypesJob : IJob
{
    private readonly ILogger<SyncLegislationTypesJob> _logger;

    public SyncLegislationTypesJob(ILogger<SyncLegislationTypesJob> logger)
    {
        _logger = logger;
    }
    public Task Run(JobRequest jobRequest, DateTime date)
    {
        _logger.LogInformation("Starting synchronization of legislation types.");

        // TODO: Add logic to sync legislationtypes here.

        _logger.LogInformation("Finished synchronization of legislation types.");
        return Task.CompletedTask;
    }
}