using LexHarvester.Domain.Const;

namespace LexHarvester.Infrastructure.Providers.Request;

public class LegislationDocumentRequest : LexBaseRequest
{
    public LegislationDocumentData Data { get; set; } = new();
}

public class LegislationDocumentData
{
    public string Id { get; set; } = string.Empty;
    public string DocumentType { get; set; } = Constants.MEVZUAT;
}