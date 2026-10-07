using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Talaqah.Application.Common.AI;
using Talaqah.Infrastructure.Ai.Models.Anthropic;
using Talaqah.Infrastructure.Ai.Options;

namespace Talaqah.Infrastructure.Ai.Providers;

internal sealed class AnthropicProvider : IAiProvider
{
    private const string ProviderName = "Anthropic";

    private readonly HttpClient _httpClient;
    private readonly AiOptions _options;
    private readonly ILogger<AnthropicProvider> _logger;

    public AnthropicProvider(
        HttpClient httpClient,
        IOptions<AiOptions> options,
        ILogger<AnthropicProvider> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;

        if (!string.IsNullOrWhiteSpace(_options.Anthropic.ApiKey))
            _httpClient.DefaultRequestHeaders.Add("x-api-key", _options.Anthropic.ApiKey);

        if (!string.IsNullOrWhiteSpace(_options.Anthropic.Version))
            _httpClient.DefaultRequestHeaders.Add("anthropic-version", _options.Anthropic.Version);
    }

    public string Name => ProviderName;

    public async Task<AiEvaluationResult> EvaluateAsync(AiPrompt prompt, CancellationToken cancellationToken)
    {
        var request = new AnthropicRequest
        {
            model = _options.Anthropic.Model,
            max_tokens = _options.Anthropic.MaxTokens,
            system = prompt.System,
            messages =
            {
                new AnthropicMessage("user", prompt.User)
            }
        };

        _logger.LogDebug("Calling Anthropic messages API with model '{Model}'.", _options.Anthropic.Model);

        var response = await _httpClient.PostAsJsonAsync(string.Empty, request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorDetails = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError("Anthropic API failed with status {StatusCode}. Details: {Details}", response.StatusCode, errorDetails);
            throw new AiProviderException(
                ProviderName,
                $"{ProviderName} API failed with status {response.StatusCode}. Details: {errorDetails}");
        }

        var anthropicResponse = await response.Content.ReadFromJsonAsync<AnthropicResponse>(cancellationToken: cancellationToken);

        var content = anthropicResponse?.content.FirstOrDefault()?.text;

        return AiResultParser.Parse(ProviderName, content ?? string.Empty);
    }
}
