using Newtonsoft.Json;

namespace LexHarvester.Infrastructure.Providers.Request;

public class CaseLawDocumentReferenceRequest : BasePaginationRequest
{
    [JsonProperty("birimAdi")]
    public string? UnitName { get; set; }
}
