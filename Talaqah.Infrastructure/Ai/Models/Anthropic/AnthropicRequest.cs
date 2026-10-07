namespace Talaqah.Infrastructure.Ai.Models.Anthropic;

internal sealed class AnthropicRequest
{
    public string model { get; set; } = string.Empty;
    public int max_tokens { get; set; } = 1024;
    public string system { get; set; } = string.Empty;
    public List<AnthropicMessage> messages { get; set; } = new();
}

internal sealed class AnthropicMessage
{
    public string role { get; set; } = string.Empty;
    public string content { get; set; } = string.Empty;

    public AnthropicMessage() { }

    public AnthropicMessage(string role, string content)
    {
        this.role = role;
        this.content = content;
    }
}
