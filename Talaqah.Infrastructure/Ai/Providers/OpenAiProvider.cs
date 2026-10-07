using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Talaqah.Application.Common.AI;
using Talaqah.Infrastructure.Ai.Models.OpenAi;
using Talaqah.Infrastructure.Ai.Options;

namespace Talaqah.Infrastructure.Ai.Providers;

internal sealed class OpenAiProvider : IAiProvider
{
    private const string ProviderName = "OpenAI";

    private readonly HttpClient _httpClient;
    private readonly AiOptions _options;
    private readonly ILogger<OpenAiProvider> _logger;

    public OpenAiProvider(
        HttpClient httpClient,
        IOptions<AiOptions> options,
        ILogger<OpenAiProvider> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
    }

    public string Name => ProviderName;

    public async Task<AiEvaluationResult> EvaluateAsync(AiPrompt prompt, CancellationToken cancellationToken)
    {
        var request = new OpenAiRequest
        {
            model = _options.OpenAI.Model,
            messages =
            {
                new OpenAiMessage("system", prompt.System),
                new OpenAiMessage("user", prompt.User)
            }
        };

        _logger.LogDebug("Calling OpenAI chat completions with model '{Model}'.", _options.OpenAI.Model);

        var response = await _httpClient.PostAsJsonAsync("chat/completions", request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorDetails = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError("OpenAI API failed with status {StatusCode}. Details: {Details}", response.StatusCode, errorDetails);
            throw new AiProviderException(
                ProviderName,
                $"{ProviderName} API failed with status {response.StatusCode}. Details: {errorDetails}");
        }

        var openAiResponse = await response.Content.ReadFromJsonAsync<OpenAiResponse>(cancellationToken: cancellationToken);

        var content = openAiResponse?.choices.FirstOrDefault()?.message.content;

        return AiResultParser.Parse(ProviderName, content ?? string.Empty);
    }
}
