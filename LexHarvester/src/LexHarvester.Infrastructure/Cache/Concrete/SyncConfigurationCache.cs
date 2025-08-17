using AutoMapper;
using LexHarvester.Domain.DTOs;
using LexHarvester.Domain.Entities;
using LexHarvester.Infrastructure.Cache.Abstract;
using Microsoft.Extensions.DependencyInjection;
using Navend.Core.Caching.Concrete;
using Navend.Core.UOW;

namespace LexHarvester.Infrastructure.Cache.Concrete;

public class SyncConfigurationCache(IMapper mapper,
                                    IServiceProvider serviceProvider) : InMemoryCache<SyncConfigurationDto>, ISyncConfigurationCache
{
    public string CacheName => nameof(SyncConfigurationCache);

    public Task LoadAsync()
    {
        LoadData();
        return Task.CompletedTask;
    }

   protected override void LoadFromSource()
    {
        var scope = serviceProvider.CreateScope();
        var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var repository = uow.GetRepository<SyncConfiguration, int>();
        var requestEndpointConfigs = repository.GetAllAsync().Result; // Asenkron metot senkron çağrılıyor, dikkatli ol!
        if (requestEndpointConfigs == null || !requestEndpointConfigs.Any())
        {
            return;
        }
        foreach (var config in requestEndpointConfigs)
        {
            var dto = mapper.Map<SyncConfigurationDto>(config);
            Set(config.SyncName, dto);
        }
    }
}
