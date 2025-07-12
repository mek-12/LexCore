namespace LexHarvester.Infrastructure.Providers.Request;

public class CaseLawDocumentReferenceRequest : LexBaseRequest
{
    public CaseLawData Data { get; set; } = new();
}
public class CaseLawData : PagingBaseData
{
    public List<string> ItemTypeList { get; set; } = new();
}
