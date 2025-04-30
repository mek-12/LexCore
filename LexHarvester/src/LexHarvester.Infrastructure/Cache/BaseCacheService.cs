using System.Collections.Concurrent;

namespace LexHarvester.Infrastructure.Cache;

public abstract class BaseCacheService<TValue>
{
    private ConcurrentDictionary<string, TValue> _cache = new();

    protected BaseCacheService()
    {
        LoadData();
    }

    public TValue? Get(string key)
    {
        _cache.TryGetValue(key, out var value);
        return value;
    }

    public IReadOnlyDictionary<string, TValue> GetAll()
    {
        return _cache;
    }

    public void Set(string key, TValue value)
    {
        _cache[key] = value;
    }

    public void Reset()
    {
        _cache.Clear();
        LoadData();
    }

    protected abstract Dictionary<string, TValue> LoadFromSource();

    protected virtual void LoadData()
    {
        var data = LoadFromSource();
        _cache = new ConcurrentDictionary<string, TValue>(data);
    }
}