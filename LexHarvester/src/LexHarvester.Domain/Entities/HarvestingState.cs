using System.ComponentModel.DataAnnotations;
using LexHarvester.Domain.Enums;
using Navend.Core.Data;

namespace LexHarvester.Domain.Entities
{
    public class HarvestingState : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        public DocumentType DocumentType { get; set; } // Enum (Mevzuat mı İçtihat mı?)

        public string? SubType { get; set; } // Kanun, Yonetmelik gibi. Tablo'dan okunacak.

        public int CurrentPage { get; set; } = 0;

        public int Count { get; set; } = 1;
        public bool Synchronized { get; set; } = false; // Count must be equal LegislationType.Count

        public bool IsCompleted { get; set; } = false;

        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        public string? LastErrorMessage { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}