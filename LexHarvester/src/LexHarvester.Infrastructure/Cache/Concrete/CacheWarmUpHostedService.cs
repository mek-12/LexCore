namespace LexHarvester.Infrastructure.Cache;
using Microsoft.Extensions.Hosting;
public class CacheWarmUpHostedService : IHostedService
{
    private readonly IEnumerable<ICacheWarmUpService> _cacheServices;

    public CacheWarmUpHostedService(IEnumerable<ICacheWarmUpService> cacheServices)
    {
        _cacheServices = cacheServices;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (var cache in _cacheServices)
        {
            await cache.LoadAsync(); // her cache tek tek load edilir
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
