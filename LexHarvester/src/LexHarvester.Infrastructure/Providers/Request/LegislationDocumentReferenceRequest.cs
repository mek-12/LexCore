namespace LexHarvester.Infrastructure.Providers.Request;

public class LegislationDocumentReferenceRequest : LexBaseRequest
{
    public LegislationRequestData Data { get; set; } = new();
}

public class LegislationRequestData : PagingBaseData
{
    public List<string> MevzuatTurList { get; set; } = new();
}