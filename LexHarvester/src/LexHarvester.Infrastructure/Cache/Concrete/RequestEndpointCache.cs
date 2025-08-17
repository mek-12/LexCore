using LexHarvester.Domain.DTOs;
using LexHarvester.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Navend.Core.Caching.Concrete;
using Navend.Core.UOW;

namespace LexHarvester.Infrastructure.Cache;

public class RequestEndpointCache : InMemoryCache<RequestEndpointConfigDto>, IRequestEndpointCache
{
    private readonly IServiceProvider _serviceProvider;
    public RequestEndpointCache(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public string CacheName => nameof(RequestEndpointCache);

    public Task LoadAsync()
    {
        LoadData(); // Base'deki LoadFromSource çağrılır
        return Task.CompletedTask;
    }

    protected override void LoadFromSource()
    {
        var scope = _serviceProvider.CreateScope();
        var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var repository = uow.GetRepository<RequestEndpointConfig, int>();
        var requestEndpointConfigs = repository.GetAllAsync().Result; // Asenkron metot senkron çağrılıyor, dikkatli ol!
        if (requestEndpointConfigs == null || !requestEndpointConfigs.Any())
        {
            return;
        }
        foreach (var config in requestEndpointConfigs)
        {
            var dto = new RequestEndpointConfigDto
            {
                Name = config.Name,
                Url = config.Url,
                Method = config.Method,
            };
            Set(config.Name, dto);
        }
    }
}
