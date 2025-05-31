using System.Collections.Concurrent;
using LexHarvester.Domain.DTOs;
using LexHarvester.Domain.Entities;
using Navend.Core.Data;
using Navend.Core.UOW;

namespace LexHarvester.Infrastructure.Cache;

public class RequestEndpointCache : BaseCacheService<RequestEndpointConfigDto>, IRequestEndpointCache, ICacheWarmUpService
{
   private readonly IRepository<RequestEndpointConfig, int> _repository;
    public RequestEndpointCache(IUnitOfWork unitOfWork)
    {
        _repository = unitOfWork.GetRepository<RequestEndpointConfig, int>();
    }
    public string CacheName => nameof(RequestEndpointCache);

    public Task LoadAsync()
    {
        LoadData(); // Base'deki LoadFromSource çağrılır
        return Task.CompletedTask;
    }

    protected override ConcurrentDictionary<string, RequestEndpointConfigDto> LoadFromSource()
    {
        var requestEndpointConfigs = _repository.GetAsync().Result; // Asenkron metot senkron çağrılıyor, dikkatli ol!
        return new ConcurrentDictionary<string, RequestEndpointConfigDto>(); // Örnek
    }
}
