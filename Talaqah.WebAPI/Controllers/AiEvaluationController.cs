using Microsoft.AspNetCore.Mvc;
using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.AiEvaluations.Commands.EvaluateStudentResponse;

namespace Talaqah.WebAPI.Controllers;

[Route("api/ai-evaluations")]
[ApiController]
public class AiEvaluationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AiEvaluationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("evaluate")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<bool>>> EvaluateResponse(
        [FromBody] EvaluateStudentResponseCommand command,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }
}
