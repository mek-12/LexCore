using System;

namespace LexHarvester.Domain.Entities;

public class MevzuatType
{
    public int Id { get; set; } // Primary key
    public int MevzuatTurId { get; set; }
    public string MevzuatTur { get; set; }
    public string MevzuatTurAdi { get; set; }
    public int SiraNo { get; set; }
    public DateTime? LastOperationDate { get; set; }
    public int Count { get; set; }
}
