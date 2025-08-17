using LexHarvester.Domain.DTOs;
using Navend.Core.Caching.Abstract;

namespace LexHarvester.Infrastructure.Cache;

public interface IRequestEndpointCache : IBaseCache<RequestEndpointConfigDto>, ICacheWarmUpService
{

}