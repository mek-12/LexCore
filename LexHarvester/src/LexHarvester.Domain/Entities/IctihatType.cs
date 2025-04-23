namespace LexHarvester.Domain.Entities;

public class IctihatType
{
    public int Id { get; set; } // Primary key
    public string Name { get; set; }
    public string Description { get; set; }
    public int? No { get; set; }
    public int Count { get; set; }
}
