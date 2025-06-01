using LexHarvester.Domain.DTOs;

namespace LexHarvester.Infrastructure.Providers.Respose;

public class CaseLawTypeResponse
{
    public List<CaseLawTypeDto> Data { get; set; } = new List<CaseLawTypeDto>();

}
