using Newtonsoft.Json;

namespace LexHarvester.Domain.DTOs;

public class CaseLawTypeDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [JsonProperty("no")]
    public int? Number { get; set; }
    public int Count { get; set; }
}
