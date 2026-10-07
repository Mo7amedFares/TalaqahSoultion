using Talaqah.Domain.Enums;

namespace Talaqah.Application.Features.Exams.DTOs.Requests
{
    public record CreateExamQuestionRuleRequest
    (
        CefrLevel CefrLevel ,
        int NumberOfQuestions
    );
}
