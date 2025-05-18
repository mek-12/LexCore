using System;
using Navend.Core.Data;

namespace LexHarvester.Domain.Entities;

public class LegislationType : IEntity<int>
{
    public int Id { get; set; } // Primary key
    public int LegislationTypeId { get; set; } // MevzuatTurId
    public string LegislationTypeCode { get; set; } // MevzuatTur
    public string LegislationTypeTitle { get; set; } // MevzuatTurAdi
    public int OrderNumber { get; set; } // SiraNo
    public DateTime? LastOperationDate { get; set; }
    public int Count { get; set; }
}
