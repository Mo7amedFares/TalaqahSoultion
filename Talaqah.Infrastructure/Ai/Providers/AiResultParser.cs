using System.Text.Json;
using Talaqah.Application.Common.AI;

namespace Talaqah.Infrastructure.Ai.Providers;

internal static class AiResultParser
{
    private sealed class ParsedResult
    {
        public string ErrorAnalysis { get; set; } = string.Empty;
        public string Recommendations { get; set; } = string.Empty;
    }

    public static AiEvaluationResult Parse(string providerName, string json)
    {
        if (string.IsNullOrWhiteSpace(json))
            throw new AiProviderException(providerName, $"{providerName} returned an empty response.");

        ParsedResult? parsed;
        try
        {
            parsed = JsonSerializer.Deserialize<ParsedResult>(json);
        }
        catch (JsonException ex)
        {
            throw new AiProviderException(
                providerName,
                $"Failed to parse {providerName} JSON response: {ex.Message}",
                ex);
        }

        if (parsed is null)
            throw new AiProviderException(providerName, $"{providerName} response deserialized to null.");

        return new AiEvaluationResult(parsed.ErrorAnalysis, parsed.Recommendations);
    }
}
