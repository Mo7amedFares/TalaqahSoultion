using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Results;

namespace Talaqah.Application.Features.AiEvaluations.Commands.EvaluateStudentResponse;

public class EvaluateStudentResponseCommand : IRequest<Result<bool>>
{
    public int ResponseId { get; set; }
}
