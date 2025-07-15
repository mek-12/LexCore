using Navend.Core.Data;

namespace LexHarvester.Domain.Entities;

public class CaseLawDivision : IEntity<int>
{
    public int Id { get; set; }
    public string? UnitId { get; set; } // birimId
    public string ItemType { get; set; } // YARGITAYKARARI vs.
    public string Name { get; set; } // daire
    public int? Order { get; set; } // daireOrder
    public string LongName { get; set; } // daireLongName
    public string? City { get; set; } // il
    public string? District { get; set; } // ilce
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
