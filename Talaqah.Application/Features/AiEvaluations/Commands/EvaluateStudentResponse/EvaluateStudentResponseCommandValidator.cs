using FluentValidation;

namespace Talaqah.Application.Features.AiEvaluations.Commands.EvaluateStudentResponse;

public class EvaluateStudentResponseCommandValidator
    : AbstractValidator<EvaluateStudentResponseCommand>
{
    public EvaluateStudentResponseCommandValidator()
    {
        RuleFor(x => x.ResponseId)
            .GreaterThan(0);
    }
}
