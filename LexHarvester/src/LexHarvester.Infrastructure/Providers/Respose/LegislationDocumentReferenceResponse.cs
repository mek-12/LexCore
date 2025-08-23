using LexHarvester.Infrastructure.Providers.Respose.BaseModels;
using Newtonsoft.Json;

namespace LexHarvester.Infrastructure.Providers.Response;

public class LegislationDocumentReferenceResponse
{
    public LegislationData Data { get; set; } = new();
    public ResponseMetadata Metadata { get; set; } = new();
}

public class LegislationData
{
    [JsonProperty("MevzuatList")]
    public List<LegislationDto> Legislations { get; set; } = new();
    public int Total { get; set; }
    public int Start { get; set; }
}
public class LegislationDto
{
    [JsonProperty("mevzuatId")]
    public string LegislationId { get; set; }

    [JsonProperty("mevzuatNo")]
    public string LegislationNumber { get; set; }

    [JsonProperty("mevzuatAdi")]
    public string LegislationTitle { get; set; }

    [JsonProperty("mevzuatTur")]
    public MevzuatTurDto LegislationTypeObject { get; set; }

    [JsonIgnore]
    public string LegislationType => LegislationTypeObject?.Name;

    [JsonIgnore]
    public int LegislationTypeId => LegislationTypeObject?.Id ?? 0;

    [JsonProperty("kayitTarihi")]
    public string RegistrationDate { get; set; }

    [JsonProperty("resmiGazeteTarihi")]
    public string OfficialGazetteDate { get; set; }

    [JsonProperty("resmiGazeteSayisi")]
    public string OfficialGazetteNumber { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }
}

public class MevzuatTurDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string MaddeSonuRegex { get; set; }
}