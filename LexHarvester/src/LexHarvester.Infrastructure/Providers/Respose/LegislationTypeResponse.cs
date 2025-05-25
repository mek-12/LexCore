using System;
using LexHarvester.Domain.DTOs;

namespace LexHarvester.Infrastructure.Providers.Respose;

public class LegislationTypeResponse
{
    public List<LegislationTypeDto> Data { get; set; } = new List<LegislationTypeDto>();
}
