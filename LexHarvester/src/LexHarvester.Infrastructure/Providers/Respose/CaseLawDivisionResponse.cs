using Newtonsoft.Json;

namespace LexHarvester.Infrastructure.Providers.Respose;

public class CaseLawDivisionResponse
{
    [JsonProperty("data")]
    public List<CaseLawDivisionData>? Data { get; set; }
}

public class CaseLawDivisionData
{
    [JsonProperty("birimId")]
    public string UnitId { get; set; }
    [JsonProperty("itemType")]
    public string ItemType { get; set; }
    [JsonProperty("daire")]
    public string Name { get; set; }
    [JsonProperty("daireLongName")]
    public string LongName { get; set; }
    [JsonProperty("daireOrder")]
    public int? Order { get; set; }
    [JsonProperty("il")]
    public string? City { get; set; }
    [JsonProperty("ilce")]
    public string? District { get; set; }
}
