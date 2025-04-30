using LexHarvester.Domain.DTOs;

namespace LexHarvester.Infrastructure.Cache;

public class RequestEndpointCache : BaseCacheService<RequestEndpointConfigDto>
{
    protected override Dictionary<string, RequestEndpointConfigDto> LoadFromSource()
    {
        throw new NotImplementedException();
    }
}