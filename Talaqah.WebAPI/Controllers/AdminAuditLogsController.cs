using Microsoft.AspNetCore.Mvc;
using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Pagination;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.AdminAuditLogs.Commands.CreateAdminAuditLog;
using Talaqah.Application.Features.AdminAuditLogs.Commands.DeleteAdminAuditLog;
using Talaqah.Application.Features.AdminAuditLogs.Commands.RestoreAdminAuditLog;
using Talaqah.Application.Features.AdminAuditLogs.DTOs;
using Talaqah.Application.Features.AdminAuditLogs.Queries.GetAllAdminAuditLog;
using Talaqah.Application.Features.AdminAuditLogs.Queries.GetAdminAuditLogById;

namespace Talaqah.WebAPI.Controllers;

[Route("api/admin-audit-logs")]
[ApiController]
public class AdminAuditLogsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminAuditLogsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets a paged list of admin audit logs.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<AdminAuditLogDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<PagedResult<AdminAuditLogDto>>>> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(
            new GetAllAdminAuditLogsQuery(new PaginationParameters(pageNumber, pageSize)),
            cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Gets an admin audit log by id.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Result<AdminAuditLogDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<AdminAuditLogDto>>> GetById(
        int id,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(
            new GetAdminAuditLogByIdQuery(id),
            cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Creates a new admin audit log.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(Result<AdminAuditLogDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<AdminAuditLogDto>>> Create(
        [FromBody] CreateAdminAuditLogCommand command,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Deletes an admin audit log.
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<bool>>> Delete(
        int id,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(
            new DeleteAdminAuditLogCommand(id),
            cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Restores a soft-deleted admin audit log.
    /// </summary>
    [HttpPatch("{id:int}/restore")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<bool>>> Restore(
        int id,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(
            new RestoreAdminAuditLogCommand(id),
            cancellationToken);

        return Ok(result);
    }
}
