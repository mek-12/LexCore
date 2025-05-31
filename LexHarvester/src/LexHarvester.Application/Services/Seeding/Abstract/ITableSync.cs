namespace LexHarvester.Application.Services.Seeding;

public interface ITableSync
{
    Task SyncAsync(CancellationToken cancellationToken = default);
}
