namespace LexHarvester.Infrastructure.Cache;

public interface ICacheWarmUpService
{
    string CacheName { get; }
    Task LoadAsync();
}