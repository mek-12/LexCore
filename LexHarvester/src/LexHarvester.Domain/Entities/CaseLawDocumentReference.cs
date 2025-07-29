using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Navend.Core.Data;

namespace LexHarvester.Domain.Entities;

// Formerly: IctihatDocument
public class CaseLawDocumentReference: IEntity<long>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; } = default;
    public string DocumentId { get; set; } = string.Empty;
    public string? ItemTypeName { get; set; }
    public string? ItemTypeDescription { get; set; }
    public string? UnitName { get; set; } // BirimAdi
    public string? DecisionNumber { get; set; } // KararNo
    public int? DecisionNumberSequence { get; set; } // KararNoSira
    public string? DecisionNumberYear { get; set; } // KararNoYil
    public string? CaseNumber { get; set; } // EsasNo
    public string? CaseNumberYear { get; set; }// EsasNoYil
    public string? CaseNumberSequence { get; set; }// EsasNoSira
    public DateTime? DecisionDate { get; set; } // KararTarihi
    public string? DecisionDateStr { get; set; }// KararTarihiStr
    public string? FinalizationStatus { get; set; }// KesinlesmeDurumu
    public string? FilePath { get; set; }
    public bool Downloaded { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}