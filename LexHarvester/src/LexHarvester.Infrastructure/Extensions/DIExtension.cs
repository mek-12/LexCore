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
        services.AddCacheWarmUpServices(new[] { typeof(RequestEndpointCache).Assembly });
        services.AddHttpClientServices();
        services.AddAutoMapper(typeof(MapperProfile));
        return services;
    }

    private static IServiceCollection AddCacheWarmUpServices(this IServiceCollection services, params Assembly[] assemblies)
    {
        var cacheTypes = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => !t.IsAbstract && typeof(ICacheWarmUpService).IsAssignableFrom(t));

        foreach (var type in cacheTypes)
        {
            var iface = type.GetInterfaces().FirstOrDefault(i => i != typeof(ICacheWarmUpService) && typeof(ICacheWarmUpService).IsAssignableFrom(i));
            if (iface != null)
                services.AddSingleton(iface, type);

            services.AddSingleton(typeof(ICacheWarmUpService), type);
        }

        services.AddHostedService<CacheWarmUpHostedService>();

        return services;
    }
    private static IServiceCollection AddHttpClientServices(this IServiceCollection services)
    {
        services.AddHttpClient<ILegislationTypeProvider,LegislationTypeProvider>(client =>
        {
            client.BaseAddress = new Uri("https://bedesten.adalet.gov.tr/");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });
        return services;
    }
}