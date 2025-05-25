using Newtonsoft.Json;

namespace LexHarvester.Infrastructure.Providers.Request;

public class BaseRequest
{
    [JsonProperty("applicationName")]
    public string ApplicationName { get; set; } = "UyapMevzuat";
}
