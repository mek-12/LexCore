using System.ComponentModel.DataAnnotations;
using LexHarvester.Domain.Enums;

namespace LexHarvester.Domain.Entities
{
    public class HarvestingState
    {
        [Key]
        public int Id { get; set; }

        public DocumentType DocumentType { get; set; } // Enum (Mevzuat mı İçtihat mı?)

        public string? SubType { get; set; } // Kanun, Yonetmelik gibi. Tablo'dan okunacak.

        public int CurrentPage { get; set; }

        public int TotalPage { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        public string? LastErrorMessage { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}