using LexHarvester.Infrastructure.Cache;
using LexHarvester.Infrastructure.Providers;
using LexHarvester.Infrastructure.Utils;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LexHarvester.Infrastructure.Extension;

public static class DIExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IRequestEndpointCache, RequestEndpointCache>();
        services.AddCaches(new[] { typeof(RequestEndpointCache).Assembly });
        services.AddHostedService<CacheWarmUpHostedService>();
        services.AddHttpClientServices();
        services.AddAutoMapper(typeof(MapperProfile));
        return services;
    }

    private static IServiceCollection AddCaches(this IServiceCollection services, params Assembly[] assemblies)
    {
        // Mevcut assembly'deki tüm sınıfları al
        var cacheServices = Assembly.GetExecutingAssembly().GetTypes()
            // `IBaseCacheService<>` ve `ICacheWarmUpService`'i implement eden sınıfları bul
            .Where(t => t.IsClass && !t.IsAbstract &&
                        t.GetInterfaces().Any(i =>
                            i.IsGenericType &&
                            i.GetGenericTypeDefinition() == typeof(IBaseCacheService<>)) &&
                        typeof(ICacheWarmUpService).IsAssignableFrom(t))
            .ToList();

        foreach (var service in cacheServices)
        {
            // Sınıfın implement ettiği tüm interface'leri al
            var interfaces = service.GetInterfaces();

            // Bu sınıfı, her interface ile register et (IBaseCacheService<T> ve ICacheWarmUpService)
            foreach (var interfaceType in interfaces)
            {
                if (interfaceType != typeof(ICacheWarmUpService) &&
                   (!interfaceType.IsGenericType ||
                     interfaceType.GetGenericTypeDefinition() != typeof(IBaseCacheService<>)))
                {
                    services.AddSingleton(interfaceType, service);
                    services.AddSingleton<ICacheWarmUpService>(provider =>
                        (ICacheWarmUpService)provider.GetRequiredService(interfaceType));

                }
            }
        }
        return services;
    }
    private static IServiceCollection AddHttpClientServices(this IServiceCollection services)
    {
        services.AddHttpClient<ILegislationTypeProvider, LegislationTypeProvider>()
                .ConfigureHttpClient((serviceProvider, client) =>
                {
                    var cache = serviceProvider.GetRequiredService<IRequestEndpointCache>();
                    var endpointConfig = cache.Get("GetMevzuatTypes");
                    if (endpointConfig != null)
                    {
                        client.BaseAddress = new Uri($"{endpointConfig.Url}{endpointConfig.Method}");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");
                    }
                    else
                    {
                        throw new InvalidOperationException("LegislationTypeEndpoint configuration not found in cache.");
                    }
                });
        return services;
    }
}