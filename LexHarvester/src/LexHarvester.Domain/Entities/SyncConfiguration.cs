using Navend.Core.Data;

namespace LexHarvester.Domain.Entities;

public class SyncConfiguration : IEntity<int>
{
    public int Id { get; set; }
    public string SyncName { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public string? Description { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
}
