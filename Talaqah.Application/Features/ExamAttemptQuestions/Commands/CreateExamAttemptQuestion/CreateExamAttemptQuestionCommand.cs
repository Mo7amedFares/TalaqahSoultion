using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.ExamAttemptQuestions.DTOs.Requests;
using Talaqah.Application.Features.ExamAttemptQuestions.DTOs.Responses;

namespace Talaqah.Application.Features.ExamAttemptQuestions.Commands.CreateExamAttemptQuestion
{
    public record CreateExamAttemptQuestionCommand(StartExamRequest Request)
        : IRequest<Result<StartExamResponse>>;
}
