using System.Collections.Concurrent;

namespace LexHarvester.Infrastructure.Cache;

public abstract class BaseCacheService<T> : IBaseCacheService<T>
{
    private ConcurrentDictionary<string, T> _cache = new();

    protected BaseCacheService() { }

    public T? Get(string key)
    {
        _cache.TryGetValue(key, out var value);
        return value;
    }

    public IReadOnlyDictionary<string, T> GetAll()
    {
        return _cache;
    }

    public void Set(string key, T value)
    {
        _cache[key] = value;
    }

    public void Reset()
    {
        _cache.Clear();
        LoadData(); // Bu doğru mu?
    }

    protected abstract void LoadFromSource();

    protected virtual void LoadData()
    {
        LoadFromSource();
    }

    public bool TryGet(string key, out T? value)
    {
        if(IsExist(key)){
            value = _cache[key];
            return true;
        }
        value = default;
        return false;
    }

    public bool IsExist(string key)
    {
        return _cache.ContainsKey(key);
    }
}