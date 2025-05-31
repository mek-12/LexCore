namespace LexHarvester.Infrastructure.Cache;

public interface IBaseCacheService<T>
{
    T? Get(string key);
    bool TryGet(string key, out T? value);
    bool IsExist(string key);
    IReadOnlyDictionary<string, T> GetAll();
    void Set(string key, T value);
    void Reset();
}