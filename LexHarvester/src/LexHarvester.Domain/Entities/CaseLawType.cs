namespace LexHarvester.Domain.Entities;

public class CaseLawType
{
    public int Id { get; set; } // Primary key
    public string Name { get; set; }
    public string Description { get; set; }
    public int? Number { get; set; } // No
    public int Count { get; set; }
}