using Microsoft.AspNetCore.Mvc;
using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Features.Lookups.Queries.GetExamLookups;

namespace Talaqah.WebAPI.Controllers;

[ApiController]
[Route("api/lookups")]
public class LookupsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LookupsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("examSection")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetExamLookups(
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetExamLookupsQuery(),
            cancellationToken);

        return Ok(result);
    }
}