using Newtonsoft.Json;

namespace LexHarvester.Domain.DTOs;

public class LegislationTypeDto
{
    [JsonProperty("mevzuatTurId")]
    public int LegislationTypeId { get; set; } // MevzuatTurId
    [JsonProperty("mevzuatTur")]
    public string LegislationTypeCode { get; set; } = string.Empty;// MevzuatTur
    [JsonProperty("mevzuatTurAdi")]
    public string LegislationTypeTitle { get; set; } = string.Empty;// MevzuatTurAdi
    [JsonProperty("siraNo")]
    public int OrderNumber { get; set; } // SiraNo
    [JsonProperty("desktopUpdatedDate")]
    public DateTime? LastOperationDate { get; set; }
    public int Count { get; set; }
}

