using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Talaqah.Application.Common.Interfaces;
using Talaqah.Infrastructure.Services;
using Talaqah.Infrastructure.Ai; // Your existing AI namespace


namespace Talaqah.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // 1. Register HTTP context and your Current User Service
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        // 2. Chain your existing AI Infrastructure registration
        services.AddAiInfrastructure(configuration);

        return services;
    }
}