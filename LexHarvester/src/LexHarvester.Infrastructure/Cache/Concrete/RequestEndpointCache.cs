using System.Collections.Concurrent;
using LexHarvester.Domain.DTOs;

namespace LexHarvester.Infrastructure.Cache;

public class RequestEndpointCache : BaseCacheService<RequestEndpoint>, IRequestEndpointCache, ICacheWarmUpService
{
    public Task LoadAsync()
    {
        LoadData();
        return Task.CompletedTask;
    }

    protected override ConcurrentDictionary<string, RequestEndpoint> LoadFromSource()
    {
        throw new NotImplementedException();
    }
}