namespace LexHarvester.Infrastructure.Providers.Request;

public class CaseLawDocumentRequest : LexBaseRequest
{
    public CaseLawDocumentData Data { get; set; } = new();
}

public class CaseLawDocumentData
{
    public string DocumentId { get; set; } = string.Empty;
}