using Microsoft.AspNetCore.Mvc;
using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.ExamAttemptQuestions.Commands.CreateExamAttemptQuestion;
using Talaqah.Application.Features.ExamAttemptQuestions.DTOs.Requests;
using Talaqah.Application.Features.ExamAttemptQuestions.DTOs.Responses;

namespace Talaqah.WebAPI.Controllers;

[Route("api/ExamAttemptQuestion")]
[ApiController]
public class ExamAttemptQuestionController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExamAttemptQuestionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Starts an exam by randomly selecting questions based on exam rules and creating exam attempt questions.
    /// </summary>
    [HttpPost("StartExam")]
    [ProducesResponseType(typeof(Result<StartExamResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<StartExamResponse>>> StartExam(
        [FromBody] StartExamRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(
            new CreateExamAttemptQuestionCommand(request),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return Ok(result);
    }
}
