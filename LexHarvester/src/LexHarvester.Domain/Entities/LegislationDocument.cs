using System;

namespace LexHarvester.Domain.Entities;

// Formerly: MevzuatDocument
public class LegislationDocumentReference
{
    public int Id { get; set; }
    public string LegislationId { get; set; } // MevzuatId
    public string LegislationNumber { get; set; } // MevzuatNo
    public string LegislationTitle { get; set; } // MevzuatAdi
    public string LegislationType { get; set; } // MevzuatTur
    public int LegislationTypeId { get; set; } // MevzuatTurId
    public DateTime? RegistrationDate { get; set; } // KayitTarihi
    public DateTime? OfficialGazetteDate { get; set; } // ResmiGazeteTarihi
    public string OfficialGazetteNumber { get; set; } // ResmiGazeteSayisi
    public string Url { get; set; }
    public string FilePath { get; set; }
    public bool Downloaded { get; set; }
    public bool Embedded { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}