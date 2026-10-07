using Microsoft.AspNetCore.Mvc;
using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Pagination;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.Users.Commands.CreateUser;
using Talaqah.Application.Features.Users.Commands.DeleteUser;
using Talaqah.Application.Features.Users.Commands.RestoreUser;
using Talaqah.Application.Features.Users.Commands.UpdateUser;
using Talaqah.Application.Features.Users.DTOs;
using Talaqah.Application.Features.Users.Queries.GetAllUsers;
using Talaqah.Application.Features.Users.Queries.GetUserById;

namespace Talaqah.WebAPI.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets a paged list of users.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<UserDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<PagedResult<UserDto>>>> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(
            new GetAllUsersQuery(new PaginationParameters(pageNumber, pageSize)),
            cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Gets user by id.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Result<UserDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<UserDto>>> GetById(
        int id,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(
            new GetUserByIdQuery(id),
            cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(Result<UserDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<UserDto>>> Create(
        CreateUserCommand command,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(Result<UserDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<UserDto>>> Update(
        int id,
        UpdateUserCommand command,
        CancellationToken cancellationToken = default)
    {
        if (id != command.Id)
            return Ok(Result<UserDto>.Failure("Route id and request id must match."));

        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Deletes a user.
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<bool>>> Delete(
        int id,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(
            new DeleteUserCommand(id),
            cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Restores a soft-deleted user.
    /// </summary>
    [HttpPatch("{id:int}/restore")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<bool>>> Restore(
        int id,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(
            new RestoreUserCommand(id),
            cancellationToken);

        return Ok(result);
    }
}
