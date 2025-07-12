using Navend.Core.Data;

namespace LexHarvester.Domain.Entities;

// Formerly: IctihatDocument
public class CaseLawDocumentReference: IEntity<long>
{
    public long Id { get; set; }
    public string DocumentId { get; set; } = string.Empty;
    public string ItemTypeName { get; set; } = string.Empty;
    public string ItemTypeDescription { get; set; } = string.Empty;
    public string UnitName { get; set; } = string.Empty; // BirimAdi
    public string DecisionNumber { get; set; } = string.Empty; // KararNo
    public int DecisionNumberSequence { get; set; } // KararNoSira
    public string DecisionNumberYear { get; set; } = string.Empty; // KararNoYil
    public string CaseNumber { get; set; } = string.Empty; // EsasNo
    public string CaseNumberYear { get; set; } = string.Empty;// EsasNoYil
    public string CaseNumberSequence { get; set; } = string.Empty;// EsasNoSira
    public DateTime? DecisionDate { get; set; } // KararTarihi
    public string DecisionDateStr { get; set; } = string.Empty;// KararTarihiStr
    public string FinalizationStatus { get; set; } = string.Empty;// KesinlesmeDurumu
    public string FilePath { get; set; } = string.Empty;
    public bool Downloaded { get; set; }
    public bool Embedded { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}