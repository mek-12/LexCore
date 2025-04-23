using System;

namespace LexHarvester.Domain.Entities;

public class MevzuatDocument
{
    public int Id { get; set; }
    public string MevzuatId { get; set; }
    public string MevzuatNo { get; set; }
    public string MevzuatAdi { get; set; }
    public string MevzuatTur { get; set; }
    public int MevzuatTurId { get; set; }
    public DateTime? KayitTarihi { get; set; }
    public DateTime? ResmiGazeteTarihi { get; set; }
    public string ResmiGazeteSayisi { get; set; }
    public string Url { get; set; }
    public string FilePath { get; set; }
    public bool Downloaded { get; set; }
    public bool Embedded { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}