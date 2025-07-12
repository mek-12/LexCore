using System.Reflection;
using LexHarvester.Application.Contracts.CQRS;
using LexHarvester.Application.Jobs;
using LexHarvester.Application.Mapper;
using LexHarvester.Application.Seeding;
using LexHarvester.Infrastructure.Extension;
using Microsoft.Extensions.DependencyInjection;

namespace LexHarvester.Application.Extensions;

public static class DependencyInjection
{
    private static readonly List<Type> jobTypes = new List<Type>();
    private static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddCQRS();

        return services;
    }
    public static IServiceCollection RegisterServices(this IServiceCollection services, Action<IServiceCollection> action)
    {
        services.AddHfJobs();
        services.AddInfrastructureServices();
        services.AddApplication();
        services.AddSeeders();
        action(services);
        return services;
    }

    private static IServiceCollection AddCQRS(this IServiceCollection services)
    {

        var assembly = Assembly.GetExecutingAssembly();

        var commandHandlerTypes = assembly.GetTypes()
                                          .Where(t => t.GetInterfaces().Any(i => i.IsGenericType &&
                                                                            i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)))
            .ToList();

        foreach (var handlerType in commandHandlerTypes)
        {
            var interfaceType = handlerType.GetInterfaces()
                .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>));

            services.AddScoped(interfaceType, handlerType);
        }

        services.AddScoped<ICommandSender, CommandSender>();
        services.AddScoped<IQuerySender, QuerySender>();
        services.AddAutoMapper(typeof(LegislationMappingProfile).Assembly); // TO DO: NavendCore a al sonr. Profile dan türeyen tüm assemblyler i tara ve ekle.
        return services;
    }

    private static IServiceCollection AddHfJobs(this IServiceCollection services)
    {
        AddJobs(services);

        services.AddTransient<Func<string, IJob>>(
            provider => key =>
            {
                var jobTypes = GetJobTypes();

                var jobType = jobTypes.FirstOrDefault(t => t.Name.Equals(key, StringComparison.OrdinalIgnoreCase));
                if (jobType != null)
                {
                    var job = provider.GetService(jobType) as IJob;
                    if (job != null)
                        return job;
                    throw new InvalidOperationException($"{jobType.Name} is not registered.");
                }

                throw new NotImplementedException($"There is no implementation for: {key}");
            }
        );
        return services;
    }
    private static IServiceCollection AddJobs(IServiceCollection services)
    {
        var jobTypes = GetJobTypes();
        foreach (var jobType in jobTypes)
        {
            if (jobType is null) continue;
            services.AddTransient(jobType);
        }
        return services;
    }

    private static IEnumerable<Type> GetJobTypes()
    {
        if (jobTypes.Any())
            return jobTypes;
        var assembly = Assembly.GetExecutingAssembly();
        jobTypes.AddRange(assembly.GetTypes()
                                   .Where(t => typeof(IJob).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract));
        return jobTypes;
    }

    private static IServiceCollection AddSeeders(this IServiceCollection services)
    {
        var seederTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => typeof(ITableSync).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        foreach (var seederType in seederTypes)
        {
            services.AddTransient(typeof(ITableSync), seederType);
        }
        services.AddTransient<ITableSyncOrchestrator, TableSyncOrchestrator>();
        return services;
    }
}