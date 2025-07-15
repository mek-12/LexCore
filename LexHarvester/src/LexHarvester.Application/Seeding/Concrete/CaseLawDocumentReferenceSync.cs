namespace LexHarvester.Application.Seeding.Concrete;

public class CaseLawDocumentReferenceSync : ITableSync
{
    public int Order => 2001;

    public Task SyncAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
