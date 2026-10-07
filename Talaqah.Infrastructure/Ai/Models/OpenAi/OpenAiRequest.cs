namespace Talaqah.Infrastructure.Ai.Models.OpenAi;

internal sealed class OpenAiRequest
{
    public string model { get; set; } = string.Empty;
    public List<OpenAiMessage> messages { get; set; } = new();
    public OpenAiResponseFormat response_format { get; set; } = new();
}

internal sealed class OpenAiMessage
{
    public string role { get; set; } = string.Empty;
    public string content { get; set; } = string.Empty;

    public OpenAiMessage() { }

    public OpenAiMessage(string role, string content)
    {
        this.role = role;
        this.content = content;
    }
}

internal sealed class OpenAiResponseFormat
{
    public string type { get; set; } = "json_object";
}
