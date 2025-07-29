using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LexHarvester.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Navend.Core.Data;

namespace LexHarvester.Domain.Entities;

[Index(nameof(ReferenceId), nameof(DocumentType), IsUnique = true)]
public class LexDocument : IEntity<long>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [Required]
    public string ReferenceId { get; set; } = string.Empty; // DocumentId veya LegislationId
    [Required]
    public DocumentType DocumentType { get; set; }
    public string? Title { get; set; } // Türkçe karakterlerden arındırılmış, formatlanmış
    public byte[]? Content { get; set; } // PDF veya HTML içeriği
    public string? FileType { get; set; } // "pdf", "html"
    public DateTime DownloadedAt { get; set; } = DateTime.UtcNow;
}
