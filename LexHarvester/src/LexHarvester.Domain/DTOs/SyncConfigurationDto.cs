namespace LexHarvester.Domain.DTOs;

public class SyncConfigurationDto
{
    public string SyncName { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public string? Description { get; set; }
}
