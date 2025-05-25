namespace LexHarvester.Domain.DTOs;

public class LegislationTypeDto
{
    public int LegislationTypeId { get; set; } 
    public string LegislationTypeCode { get; set; } = string.Empty;
    public string LegislationTypeTitle { get; set; } = string.Empty;
    public int OrderNumber { get; set; } 
    public DateTime? LastOperationDate { get; set; }
    public int Count { get; set; }
}

