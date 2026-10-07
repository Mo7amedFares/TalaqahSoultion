using Talaqah.Application.Common.AI;
using Talaqah.Domain.Entities;

namespace Talaqah.Application.Common.Interfaces;

public interface IAiEvaluationService
{
    Task<AiEvaluationResult> EvaluateStudentResponseAsync(
        StudentResponse studentResponse,
        CancellationToken cancellationToken = default);
}
