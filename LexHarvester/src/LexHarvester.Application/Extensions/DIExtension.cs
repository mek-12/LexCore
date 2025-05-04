using System.Reflection;
using LexHarvester.Application.Contracts.CQRS;
using Microsoft.Extensions.DependencyInjection;

namespace LexHarvester.Application.Extensions;
public static class DependencyInjection
    {
        private static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddCQRS();

            return services;
        }
        public static IServiceCollection AddInfras(this IServiceCollection services, Action<IServiceCollection> action){
            services.AddApplication();
            action(services);
            return services;
        }
        
        private static IServiceCollection AddCQRS(this IServiceCollection services){
            
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

            services.AddScoped<ICommandSender, CommandSender>();
            services.AddScoped<IQuerySender, QuerySender>();
            return services;
        }
    }