using Navend.Core.Data;

namespace LexHarvester.Domain.Entities;

public class CaseLawType :IEntity<int>
{
    public int Id { get; set; } // Primary key
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int? Number { get; set; } // No
    public int Count { get; set; }
}