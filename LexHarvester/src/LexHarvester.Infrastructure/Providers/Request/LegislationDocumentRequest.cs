namespace LexHarvester.Infrastructure.Providers.Request;

public class LegislationDocumentRequest : LexBaseRequest
{
    public LegislationDocumentData Data { get; set; } = new();
}

public class LegislationDocumentData
{
    public string DocumentId { get; set; } = string.Empty;
}