using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Talaqah.Application.Common.Mediator;

namespace Talaqah.WebAPI.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationHandlers(
        this IServiceCollection services)
    {
        var assembly = Assembly.Load("Talaqah.Application");

        var handlerTypes = assembly
            .GetTypes()
            .Where(type =>
                type.IsClass &&
                !type.IsAbstract &&
                type.GetInterfaces()
                    .Any(i =>
                        i.IsGenericType &&
                        i.GetGenericTypeDefinition() ==
                        typeof(IRequestHandler<,>)))
            .ToList();

        foreach (var handlerType in handlerTypes)
        {
            var interfaceType = handlerType
                .GetInterfaces()
                .First(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() ==
                    typeof(IRequestHandler<,>));

            services.AddScoped(interfaceType, handlerType);
        }

        return services;
    }
}