using Hangfire;

namespace LexHarvester.API.Extensions;

public static class DIExtension {
    internal static IServiceCollection AddHf(this IServiceCollection services, string? connectionString) {
        services.AddHangfire(config => {
            config.UseSqlServerStorage(connectionString);
        });
        services.AddHangfireServer();
        return services;
    }
}