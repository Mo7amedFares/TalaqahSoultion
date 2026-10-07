using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Talaqah.Application.Common.Interfaces;
using Talaqah.Persistence.Contexts;

namespace Talaqah.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("TalaqahDB")
            ?? throw new InvalidOperationException(
                "Database connection string 'ConnectionStrings:TalaqahDB' is not configured. " +
                "Add it under 'ConnectionStrings' in appsettings.json.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                connectionString,
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}