namespace Talaqah.Application.Common.AI;

public sealed record AiEvaluationResult
{
    public string ErrorAnalysis { get; init; } = string.Empty;
    public string Recommendations { get; init; } = string.Empty;

    public AiEvaluationResult() { }

    public AiEvaluationResult(string errorAnalysis, string recommendations)
    {
        ErrorAnalysis = errorAnalysis ?? string.Empty;
        Recommendations = recommendations ?? string.Empty;
    }
}
