using Navend.Core.Data;

namespace LexHarvester.Domain.Entities;

// Formerly: MevzuatDocument
public class LegislationDocumentReference: IEntity<long>
{
    public long Id { get; set; }
    public string LegislationId { get; set; } = string.Empty; // MevzuatId
    public string? LegislationNumber { get; set; } = string.Empty;// MevzuatNo
    public string? LegislationTitle { get; set; } = string.Empty;// MevzuatAdi
    public string? LegislationType { get; set; } = string.Empty;// MevzuatTur
    public int? LegislationTypeId { get; set; } // MevzuatTurId
    public DateTime? RegistrationDate { get; set; } // KayitTarihi
    public DateTime? OfficialGazetteDate { get; set; } // ResmiGazeteTarihi
    public string? OfficialGazetteNumber { get; set; } = string.Empty; // ResmiGazeteSayisi
    public string? Url { get; set; } = string.Empty;
    public string? FilePath { get; set; } = string.Empty;
    public bool Downloaded { get; set; }
    public bool Embedded { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}