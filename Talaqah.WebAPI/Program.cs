using Talaqah.Application;
using Talaqah.Application.Common.Interfaces;
using Talaqah.Infrastructure.Ai;
using Talaqah.Infrastructure;
using Talaqah.Persistence;
using Talaqah.WebAPI.Extensions;
using Talaqah.WebAPI.Extensions.ExceptionHandler;
using Talaqah.WebAPI.Extensions.JWTAuthentication;

namespace Talaqah.WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler =
                    System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });

        builder.Services.AddOpenApi("v1", options =>
        {
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("FrontendPolicy", policy =>
            {
                policy
                    .WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        builder.Services.AddPersistenceInfrastructure(
            builder.Configuration
        );

        builder.Services.AddApplicationServices();

        builder.Services.AddApplicationHandlers();

        builder.Services.AddInfrastructureServices(
            builder.Configuration
        );

        // builder.Services.AddHttpContextAccessor();
        // builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

        builder.Services.AddProblemDetails();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(
                    "/openapi/v1.json",
                    "My API v1"
                );

                options.EnablePersistAuthorization();
            });
        }

        app.UseHttpsRedirection();

        app.UseExceptionHandler();

        app.UseCors("FrontendPolicy");

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}