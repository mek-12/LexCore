using Newtonsoft.Json;

namespace LexHarvester.Infrastructure.Providers.Request;

public class BaseRequest
{
    [JsonProperty("applicationName")]
    public string ApplicationName { get; set; } = "UyapMevzuat";
}

public class BasePaginationRequest : BaseRequest
{
    [JsonProperty("pageNumber")]
    public int PageNumber { get; set; } = 1;

    [JsonProperty("pageSize")]
    public int PageSize { get; set; } = 100;

    [JsonProperty("sortBy")]
    public List<string> SortFields { get; set; } = new List<string>();

    [JsonProperty("sortDirection")]
    public string SortDirection { get; set; } = "desc";
    [JsonProperty("paging")]
    public bool IsPaginationEnabled { get; set; } = true;
}