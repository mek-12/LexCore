using Microsoft.Extensions.Logging;

namespace LexHarvester.Infrastructure.Services.Seeding;

public class SeederOrchestrator
{
    private readonly IEnumerable<ITableSeeder> _seeders;
    private readonly ILogger<SeederOrchestrator> _logger;

    public SeederOrchestrator(IEnumerable<ITableSeeder> seeders, ILogger<SeederOrchestrator> logger)
    {
        _seeders = seeders;
        _logger = logger;
    }

    public async Task RunAllSeedersAsync(CancellationToken cancellationToken = default)
    {
        foreach (var seeder in _seeders)
        {
            try
            {
                var seederType = seeder.GetType().Name;
                _logger.LogInformation("Seeder started: {Seeder}", seederType);

                await seeder.SeedIfTableEmptyAsync(cancellationToken);

                _logger.LogInformation("Seeder finished: {Seeder}", seederType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Seeder failed: {Seeder}", seeder.GetType().Name);
            }
        }
    }
}
