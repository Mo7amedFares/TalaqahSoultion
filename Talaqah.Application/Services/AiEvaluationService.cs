using Talaqah.Application.Common.AI;
using Talaqah.Application.Common.Interfaces;
using Talaqah.Domain.Entities;

namespace Talaqah.Application.Services;

public sealed class AiEvaluationService : IAiEvaluationService
{
    private const string SystemPrompt = @"You are an expert English language evaluator aligned with CEFR standards. 
        Your task is to evaluate the student's answer based on the provided question.
        You MUST respond ONLY with a JSON object containing exactly two string properties: 
        'ErrorAnalysis' (explaining grammar and spelling errors as a JSON string) and 
        'Recommendations' (providing next steps as a JSON string).";

    private readonly IAiProvider _aiProvider;

    public AiEvaluationService(IAiProvider aiProvider)
    {
        _aiProvider = aiProvider;
    }

    public Task<AiEvaluationResult> EvaluateStudentResponseAsync(
        StudentResponse studentResponse,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(studentResponse);

        var questionText = studentResponse.ExamAttemptQuestion?.Question?.QuestionText ?? string.Empty;
        var userPrompt = $"Question: {questionText}\nStudent Answer: {studentResponse.TextResponse}";

        var prompt = new AiPrompt(SystemPrompt, userPrompt);

        return _aiProvider.EvaluateAsync(prompt, cancellationToken);
    }
}
