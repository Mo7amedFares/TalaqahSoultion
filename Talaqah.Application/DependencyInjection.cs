using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Talaqah.Application.Common.Interfaces;
using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Services;

namespace Talaqah.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IMediator, Mediator>();

        services.AddValidatorsFromAssembly(
            Assembly.Load("Talaqah.Application"),
            ServiceLifetime.Scoped);

        services.AddScoped<IAiEvaluationService, AiEvaluationService>();

        return services;
    }
}
