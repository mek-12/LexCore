namespace LexHarvester.Infrastructure.Cache;

public interface ICacheWarmUpService
{
    Task LoadAsync();
}