using Newtonsoft.Json;

namespace LexHarvester.Infrastructure.Providers.Request;

public class LegislationDocumentReferenceResponse
{
    public LegislationData Data { get; set; }
    public ResponseMetadata Metadata { get; set; }
}

public class LegislationData
{
    [JsonProperty("MevzuatList")]
    public List<LegislationDto> Legislations { get; set; }
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
    public DateTime? RegistrationDate { get; set; }

    [JsonProperty("resmiGazeteTarihi")]
    public DateTime? OfficialGazetteDate { get; set; }

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

public class ResponseMetadata
{
    public string FMTY { get; set; }
    public string FMC { get; set; }
    public string FMTE { get; set; }
    public string FMU { get; set; }
    public string PTID { get; set; }
    public string TID { get; set; }
    public string SID { get; set; }
}
