using Microsoft.Extensions.DependencyInjection;

namespace LexHarvester.Persistence.Extensions;
public static class DIExtension {
    public static IServiceCollection AddRepositories(this IServiceCollection services, Action<IServiceCollection> action) {
        action(services);
        return services;
    }
}