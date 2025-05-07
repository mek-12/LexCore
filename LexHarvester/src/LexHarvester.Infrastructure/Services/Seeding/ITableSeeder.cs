namespace LexHarvester.Infrastructure.Services.Seeding;

public interface ITableSeeder
{
    Task SeedIfTableEmptyAsync(CancellationToken cancellationToken = default);
}
