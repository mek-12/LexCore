using LexHarvester.Helper.Utils.JsonUtils;
using Newtonsoft.Json;

namespace LexHarvester.Domain.DTOs;

public class CaseLawDocumentReferenceDto
{
    public string? DocumentId { get; set; }
    public ItemType? ItemType { get; set; }
    [JsonProperty("birimId")]
    public string? UnitId { get; set; } // BirimId
    [JsonProperty("birimAdi")]
    public string? UnitName { get; set; } // BirimAdi
    [JsonProperty("esasNoYil")]
    public string? CaseNumberYear { get; set; } // EsasNoYil
    [JsonProperty("esasNoSira")]
    public string? CaseNumberSequence { get; set; } // EsasNoSira
    [JsonProperty("kararNoYil")]
    public string? DecisionNumberYear { get; set; } // KararNoYil
    [JsonProperty("kararNoSira")]
    public int? DecisionNumberSequence { get; set; } // KararNoSira
    [JsonProperty("kararTuru")]
    public string? DecisionType { get; set; } // KararTürü
    [JsonProperty("kararTarihi")]
    [JsonConverter(typeof(SafeDateTimeConverter))]
    public DateTime? DecisionDate { get; set; } // KararTarihi
    [JsonProperty("kararTarihiStr")]
    public string? DecisionDateStr { get; set; } // KararTarihiStr
    [JsonProperty("kesinlesmeDurumu")]
    public string? FinalizationStatus { get; set; } // KesinlesmeDurumu
    [JsonProperty("kararNo")]
    public string? DecisionNumber { get; set; } // KararNo
    [JsonProperty("esasNo")]
    public string? CaseNumber { get; set; } // EsasNo
}

public class ItemType
{
    public string? Name { get; set; }

    public string? Description { get; set; }
}