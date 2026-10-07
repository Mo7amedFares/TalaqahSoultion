namespace Talaqah.Infrastructure.Ai.Models.Anthropic;

internal sealed class AnthropicResponse
{
    public List<AnthropicContent> content { get; set; } = new();
}

internal sealed class AnthropicContent
{
    public string text { get; set; } = string.Empty;
}
