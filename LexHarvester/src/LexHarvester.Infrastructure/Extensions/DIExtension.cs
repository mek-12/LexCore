using LexHarvester.Infrastructure.Cache;
using LexHarvester.Infrastructure.Services.Seeding;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LexHarvester.Infrastructure.Extension;

public static class DIExtension {
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services) {
        services.AddScoped<ITableSeeder, LegislationTypeSeeder>();
        services.AddSingleton<IRequestEndpointCache, RequestEndpointCache>();
        services.AddCacheWarmUpServices(Assembly.GetExecutingAssembly());
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
}