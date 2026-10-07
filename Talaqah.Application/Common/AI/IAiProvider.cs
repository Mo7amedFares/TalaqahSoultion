namespace Talaqah.Application.Common.AI;

public interface IAiProvider
{
    string Name { get; }

    Task<AiEvaluationResult> EvaluateAsync(AiPrompt prompt, CancellationToken cancellationToken = default);
}
