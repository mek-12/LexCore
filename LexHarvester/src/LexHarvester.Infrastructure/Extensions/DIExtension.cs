using LexHarvester.Infrastructure.Cache;
using LexHarvester.Infrastructure.Cache.Abstract;
using LexHarvester.Infrastructure.Cache.Concrete;
using LexHarvester.Infrastructure.Mapper;
using LexHarvester.Infrastructure.Providers;
using LexHarvester.Infrastructure.Providers.Abstract;
using LexHarvester.Infrastructure.Providers.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LexHarvester.Infrastructure.Extension;

public static class DIExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping)); // TO DO: NavendCore a al sonr. Profile dan türeyen tüm assemblyler i tara ve ekle.
        services.AddCaches(new[] { typeof(RequestEndpointCache).Assembly }); // TO DO: Move cache mechanism to Navend.Core. Even create a new library named as Navend.Cache
        services.AddHostedService<CacheWarmUpHostedService>();
        services.AddHttpClientServices();
        return services;
    }

    private static IServiceCollection AddCaches(this IServiceCollection services, params Assembly[] assemblies)
    {
        // Mevcut assembly'deki tüm sınıfları al
        services.AddSingleton<IRequestEndpointCache, RequestEndpointCache>();
        services.AddSingleton<ISyncConfigurationCache, SyncConfigurationCache>();
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
        // TO DO: Navend.Core a al
        services.AddConfiguredHttpClient<ILegislationTypeProvider, LegislationTypeProvider>("GetMevzuatTypes");
        services.AddConfiguredHttpClient<ICaseLawTypeProvider, CaseLawTypeProvider>("GetIctihatTypes");
        services.AddConfiguredHttpClient<ICaseLawDocumentReferenceProvider, CaseLawDocumentReferenceProvider>("GetIctihatDocumentReferences");
        services.AddConfiguredHttpClient<ILegislationDocumentReferenceProvider, LegislationDocumentReferenceProvider>("GetLegislationDocumentReferences");
        services.AddConfiguredHttpClient<ICaseLawDivisionProvider, CaseLawDivisionProvider>("GetCaseLawDivision"); 

        return services;
    }
    private static IHttpClientBuilder AddConfiguredHttpClient<TInterface, TImplementation>(
        this IServiceCollection services,
        string endpointKey)
        where TImplementation : class, TInterface
        where TInterface : class
    {
        return services.AddHttpClient<TInterface, TImplementation>()
            .ConfigureHttpClient((serviceProvider, client) =>
            {
                var cache = serviceProvider.GetRequiredService<IRequestEndpointCache>();
                var endpointConfig = cache.Get(endpointKey);

                if (endpointConfig == null)
                {
                    throw new InvalidOperationException($"{typeof(TInterface).Name} endpoint configuration '{endpointKey}' not found in cache.");
                }

                client.BaseAddress = new Uri($"{endpointConfig.Url}{endpointConfig.Method}");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
    }
}