using System;

namespace LexHarvester.Domain.Entities;

public class IctihatDocument
{
    public int Id { get; set; }
    public string DocumentId { get; set; }
    public string ItemType { get; set; }
    public string BirimAdi { get; set; }
    public string KararNo { get; set; }
    public string EsasNo { get; set; }
    public DateTime? KararTarihi { get; set; }
    public string KararTarihiStr { get; set; }
    public string KesinlesmeDurumu { get; set; }
    public string FilePath { get; set; }
    public bool Downloaded { get; set; }
    public bool Embedded { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
