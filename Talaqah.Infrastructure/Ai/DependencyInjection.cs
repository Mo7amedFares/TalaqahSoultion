using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Talaqah.Application.Common.AI;
using Talaqah.Infrastructure.Ai.Options;
using Talaqah.Infrastructure.Ai.Providers;

namespace Talaqah.Infrastructure.Ai;

public static class DependencyInjection
{
    public static IServiceCollection AddAiInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<AiOptions>(configuration.GetSection(AiOptions.SectionName));

        services.PostConfigure<AiOptions>(options =>
        {
            if (string.IsNullOrWhiteSpace(options.OpenAI.ApiKey))
                options.OpenAI.ApiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? string.Empty;

            if (string.IsNullOrWhiteSpace(options.Anthropic.ApiKey))
                options.Anthropic.ApiKey = Environment.GetEnvironmentVariable("ANTHROPIC_API_KEY") ?? string.Empty;
        });

        var aiSection = configuration.GetSection(AiOptions.SectionName);

        services.AddHttpClient<OpenAiProvider>(client =>
            ConfigureOpenAi(client, aiSection));

        services.AddHttpClient<AnthropicProvider>(client =>
            ConfigureAnthropic(client, aiSection));

        services.AddScoped<IAiProvider>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<AiOptions>>().Value;
            var useAnthropic = string.Equals(
                options.DefaultProvider,
                "Anthropic",
                StringComparison.OrdinalIgnoreCase);

            return useAnthropic
                ? sp.GetRequiredService<AnthropicProvider>()
                : sp.GetRequiredService<OpenAiProvider>();
        });

        return services;
    }

    private static void ConfigureOpenAi(HttpClient client, IConfigurationSection section)
    {
        var openAi = section.GetSection("OpenAI");
        client.BaseAddress = new Uri(openAi["ApiUrl"] ?? "https://api.openai.com/v1/");

        var key = ResolveKey(openAi["ApiKey"], "OPENAI_API_KEY");
        if (!string.IsNullOrWhiteSpace(key))
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {key}");
    }

    private static void ConfigureAnthropic(HttpClient client, IConfigurationSection section)
    {
        var anthropic = section.GetSection("Anthropic");
        client.BaseAddress = new Uri(anthropic["ApiUrl"] ?? "https://api.anthropic.com/v1/messages");
    }

    private static string? ResolveKey(string? configValue, string envVar)
        => string.IsNullOrWhiteSpace(configValue)
            ? Environment.GetEnvironmentVariable(envVar)
            : configValue;
}
