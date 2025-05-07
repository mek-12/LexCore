using LexHarvester.Persistence.Repositories;
using LexHarvester.Persistence.UOW;
using Microsoft.Extensions.DependencyInjection;

namespace LexHarvester.Persistence.Extensions;
public static class DIExtension {
    public static IServiceCollection AddRepositories(this IServiceCollection services, Action<IServiceCollection> action) {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        action(services);
        return services;
    }
}