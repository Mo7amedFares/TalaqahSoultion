namespace Talaqah.Infrastructure.Ai.Options;

public sealed class AiOptions
{
    public const string SectionName = "Ai";

    public string DefaultProvider { get; set; } = "OpenAI";

    public OpenAiOptions OpenAI { get; set; } = new();

    public AnthropicOptions Anthropic { get; set; } = new();
}

public sealed class OpenAiOptions
{
    public string ApiKey { get; set; } = string.Empty;
    public string ApiUrl { get; set; } = "https://api.openai.com/v1/";
    public string Model { get; set; } = "gpt-4o-mini";
}

public sealed class AnthropicOptions
{
    public string ApiKey { get; set; } = string.Empty;
    public string ApiUrl { get; set; } = "https://api.anthropic.com/v1/messages";
    public string Version { get; set; } = "2023-06-01";
    public string Model { get; set; } = "claude-3-5-sonnet-20240620";
    public int MaxTokens { get; set; } = 1024;
}
