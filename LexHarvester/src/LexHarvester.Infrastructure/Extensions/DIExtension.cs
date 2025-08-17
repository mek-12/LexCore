using LexHarvester.Infrastructure.Cache;
using LexHarvester.Infrastructure.Mapper;
using LexHarvester.Infrastructure.Providers;
using LexHarvester.Infrastructure.Providers.Abstract;
using LexHarvester.Infrastructure.Providers.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Navend.Core.Constants;
using Navend.Core.Extensions;

namespace LexHarvester.Infrastructure.Extension;

public static class DIExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(AutoMapping)); // TO DO: NavendCore a al sonr. Profile dan türeyen tüm assemblyler i tara ve ekle.
        var cacheType = CacheTypes.InMemory | CacheTypes.Redis;
        services.AddCaches(configuration, cacheType,  new[] {typeof(RequestEndpointCache).Assembly }); // TO DO: Move cache mechanism to Navend.Core. Even create a new library named as Navend.Cache
        services.AddHttpClientServices();
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
        services.AddConfiguredHttpClient<ILegislationDocumentProvider, LegislationDocumentProvider>("GetLegislationDocuments"); 
        // services.AddConfiguredHttpClient<ICaseLawDocumentProvider, CaseLawDocumentProvider>("GetCaseLawDocuments"); 
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