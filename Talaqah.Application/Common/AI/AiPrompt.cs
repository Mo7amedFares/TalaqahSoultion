namespace Talaqah.Application.Common.AI;

public sealed record AiPrompt(string System, string User)
{
    public static AiPrompt Empty { get; } = new(string.Empty, string.Empty);
}
