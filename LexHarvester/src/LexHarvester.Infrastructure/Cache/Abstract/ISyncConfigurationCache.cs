using LexHarvester.Domain.DTOs;
using Navend.Core.Caching.Abstract;

namespace LexHarvester.Infrastructure.Cache.Abstract;

public interface ISyncConfigurationCache : IBaseCache<SyncConfigurationDto>, ICacheWarmUpService { }