EF Core Code First Migration:

Yeni bir ef core model eklediğimizde veya modifiye ettiğimizde:
1) önce migration dosyasını oluşturuyoruz localde 
    * dotnet ef migrations add <Migration_Name> --project ./src/LexHarvester.Persistence/LexHarvester.Persistence.csproj
2) 'docker compose up --build' komutu ile tekrar container ı ayağa kaldırıyoruz.

Not: Migration işlemini yaptıktan sonra ileride güvenlik problemi olmasın diye ApplicationDbContextFactory de ki ConnectionString i silelim.


# LexHarvester - Extensible Generic Cache System

This system ensures that when the application starts, certain database tables are checked for data. If they're empty, the system populates them and then loads the cache using that data. All cache services implementing `ICacheWarmUpService` are invoked at startup.

---

## 🎯 Purpose

- Check whether essential tables have data at application startup
- If empty, fetch the data from external sources (e.g., HTTP APIs) and populate the database
- Populate the in-memory cache from the database
- Provide singleton cache services for dependency injection across the app

---

## 🧱 Core Components

### `ICacheWarmUpService`

```csharp
public interface ICacheWarmUpService
{
    Task LoadAsync();
}
```

### `BaseCacheService<T>`

```csharp
public abstract class BaseCacheService<T> : IBaseCacheService<T>
{
    protected ConcurrentDictionary<string, T> _cache = new();

    public T? Get(string key) => _cache.TryGetValue(key, out var value) ? value : default;
    public IReadOnlyDictionary<string, T> GetAll() => _cache;
    public void Clear() => _cache.Clear();

    public void LoadData()
    {
        _cache = new ConcurrentDictionary<string, T>(LoadFromSource());
    }

    protected abstract ConcurrentDictionary<string, T> LoadFromSource();
}
```

---

## 🧠 Example: `RequestEndpointCache`

```csharp
public class RequestEndpointCache : BaseCacheService<RequestEndpoint>, IRequestEndpointCache, ICacheWarmUpService
{
    private readonly IUnitOfWork _uow;

    public RequestEndpointCache(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task LoadAsync()
    {
        var repo = _uow.GetRepository<RequestEndpointConfig>();
        var configs = await repo.GetAllAsync();
        var items = configs.Select(x => x.AsDTO()).ToList();
        _cache = new ConcurrentDictionary<string, RequestEndpoint>(items.ToDictionary(x => x.Name));
    }

    protected override ConcurrentDictionary<string, RequestEndpoint> LoadFromSource()
    {
        throw new NotImplementedException(); // LoadAsync is preferred
    }
}
```

---

## 🚀 `CacheWarmUpHostedService`

```csharp
public class CacheWarmUpHostedService : IHostedService
{
    private readonly IEnumerable<ICacheWarmUpService> _cacheServices;

    public CacheWarmUpHostedService(IEnumerable<ICacheWarmUpService> cacheServices)
    {
        _cacheServices = cacheServices;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (var cache in _cacheServices)
        {
            await cache.LoadAsync();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
```

---

## 🧩 `AddCacheWarmUpServices` Extension Method

```csharp
public static class CacheWarmUpExtensions
{
    public static IServiceCollection AddCacheWarmUpServices(this IServiceCollection services, Assembly assembly)
    {
        var types = assembly.GetTypes()
            .Where(t => typeof(ICacheWarmUpService).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        foreach (var type in types)
        {
            services.AddSingleton(typeof(ICacheWarmUpService), type);
        }

        services.AddHostedService<CacheWarmUpHostedService>();
        return services;
    }
}
```

---

## 🔧 Usage in `Program.cs`

```csharp
builder.Services.AddSingleton<IRequestEndpointCache, RequestEndpointCache>();
builder.Services.AddCacheWarmUpServices(typeof(RequestEndpointCache).Assembly);
```

---

## ✅ Important Notes

- `RequestEndpointCache` is registered as a singleton.
- The same instance is used both in `ICacheWarmUpService` (for warm-up at startup) and in services as `IRequestEndpointCache`.
- No redundant instances are created—dependency injection reuses the singleton.
- Every time the app starts, caches are refreshed automatically.
- In `LoadAsync()`, you can also make an HTTP call to retrieve data, populate the database, and then cache it—ideal for first-time deployments.
