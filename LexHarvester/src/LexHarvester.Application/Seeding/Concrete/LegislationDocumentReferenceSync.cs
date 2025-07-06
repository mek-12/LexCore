namespace LexHarvester.Application.Seeding.Concrete;

public class LegislationDocumentReferenceSync : ITableSync
{
    public int Order => 2;

    public Task SyncAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
