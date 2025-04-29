using Hangfire;

namespace LexHarvester.API.Extensions;

public static class DIExtension {
    internal static IServiceCollection AddHf(this IServiceCollection services, IConfiguration configuration) {
        services.AddHangfire(config => {
            config.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddHangfireServer();
        return services;
    }
}