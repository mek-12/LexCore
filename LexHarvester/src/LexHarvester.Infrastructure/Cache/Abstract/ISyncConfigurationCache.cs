using LexHarvester.Domain.DTOs;

namespace LexHarvester.Infrastructure.Cache.Abstract;

public interface ISyncConfigurationCache : IBaseCacheService<SyncConfigurationDto>, ICacheWarmUpService { }