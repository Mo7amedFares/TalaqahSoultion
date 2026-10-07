using Microsoft.AspNetCore.Mvc;
using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Pagination;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.Exams.Commands.CreateExam;
using Talaqah.Application.Features.Exams.DTOs;
using Talaqah.Application.Features.Exams.DTOs.Responses;
using Talaqah.Application.Features.Exams.Queries.GetAllExamByAdmin;
using Talaqah.Application.Features.Exams.Queries.GetAllExamByUserId;

namespace Talaqah.WebAPI.Controllers;

[Route("api/exams")]
[ApiController]
public class ExamsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExamsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets a paged list of exams for a given user (student dashboard).
    /// </summary>
    [HttpGet("GetAll/{id:int}")]
    [ProducesResponseType(typeof(Result<PagedResult<ExamsViewByUserIdResponse>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<PagedResult<ExamsViewByUserIdResponse>>>> GetAllByUserID(
        [FromRoute] int id,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(
            new GetAllExamByUserIdQuery(id, new PaginationParameters(pageNumber, pageSize)),
            cancellationToken);

        return Ok(result);
    }
    /// <summary>
    /// Creates a new Exam.   ///Upadte Audit and and ExamId in Questionrule
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Result<int>>> Create(
        [FromBody] CreateExamCommand command,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return result.IsSuccess? Created() : BadRequest(result.Message);
    }

    /// <summary>
    /// Gets All exams for Admin
    /// </summary>
    [HttpGet("GetAll")]
    [ProducesResponseType(typeof(Result<ExamsViewByUserIdResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<ExamsViewByUserIdResponse>>> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(
            new GetAllExamByAdminQuery(new PaginationParameters(pageNumber, pageSize)),
            cancellationToken);

        return Ok(result);
    }
}
