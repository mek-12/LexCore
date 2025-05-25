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
        services.AddCacheWarmUpServices(Assembly.GetExecutingAssembly());
        services.AddHttpClientServices();
        services.AddAutoMapper(typeof(MapperProfile));
        return services;
    }

    private static IServiceCollection AddCacheWarmUpServices(this IServiceCollection services, params Assembly[] assembliesToScan)
    {
        var interfaceType = typeof(ICacheWarmUpService);

        var typesToRegister = assembliesToScan
            .SelectMany(a => a.GetTypes())
            .Where(t => !t.IsAbstract && !t.IsInterface && interfaceType.IsAssignableFrom(t));

        foreach (var type in typesToRegister)
        {
            services.AddSingleton(interfaceType, type);
        }

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