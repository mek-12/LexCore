using System.Reflection;
using LexHarvester.Application.Contracts.CQRS;
using Microsoft.Extensions.DependencyInjection;

namespace LexHarvester.Application.Extensions;
public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddCommandHandlers();
            services.AddQueryHandlers();

            // Eğer ICommandSender, IQuerySender gibi şeyleri de burada register ediyorsan:
            services.AddScoped<ICommandSender, CommandSender>();
            services.AddScoped<IQuerySender, QuerySender>();

            return services;
        }

        private static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var commandHandlerTypes = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)))
                .ToList();

            foreach (var handlerType in commandHandlerTypes)
            {
                var interfaceType = handlerType.GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>));

                services.AddScoped(interfaceType, handlerType);
            }

            return services;
        }

        private static IServiceCollection AddQueryHandlers(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var queryHandlerTypes = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)))
                .ToList();

            foreach (var handlerType in queryHandlerTypes)
            {
                var interfaceType = handlerType.GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>));

                services.AddScoped(interfaceType, handlerType);
            }

            return services;
        }
    }