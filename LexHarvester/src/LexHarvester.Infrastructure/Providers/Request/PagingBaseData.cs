using LexHarvester.Domain.Const;

namespace LexHarvester.Infrastructure.Providers.Request;

public class PagingBaseData
{
    public int PageSize { get; set; } = Constants.PAGE_SIZE;
    public int PageNumber { get; set; } = 1;
    public List<string> SortFields { get; set; } = new () { Constants.RESMI_GAZETE_TARIHI };
    public string SortDirection { get; set; } = "desc";
}
