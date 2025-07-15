namespace LexHarvester.Infrastructure.Providers.Request;

public class CaseLawDivisionRequest : LexBaseRequest
{
    public DivisionCaseLawType Data { get; set; } = new();
}

public class DivisionCaseLawType
{
    public string ItemType { get; set; }
}