namespace Talaqah.Infrastructure.Ai.Models.OpenAi;

internal sealed class OpenAiResponse
{
    public List<OpenAiChoice> choices { get; set; } = new();
}

internal sealed class OpenAiChoice
{
    public OpenAiMessage message { get; set; } = new();
}
