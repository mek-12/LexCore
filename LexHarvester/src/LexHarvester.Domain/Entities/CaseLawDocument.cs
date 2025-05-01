using System;

namespace LexHarvester.Domain.Entities;

// Formerly: IctihatDocument
public class CaseLawDocument
{
    public int Id { get; set; }
    public string DocumentId { get; set; }
    public string ItemType { get; set; }
    public string UnitName { get; set; } // BirimAdi
    public string DecisionNumber { get; set; } // KararNo
    public string CaseNumber { get; set; } // EsasNo
    public DateTime? DecisionDate { get; set; } // KararTarihi
    public string DecisionDateStr { get; set; } // KararTarihiStr
    public string FinalizationStatus { get; set; } // KesinlesmeDurumu
    public string FilePath { get; set; }
    public bool Downloaded { get; set; }
    public bool Embedded { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
