using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.Users.DTOs;

namespace Talaqah.Application.Features.Users.Commands.UpdateUser;

public record UpdateUserCommand(
    int Id,
    string FirstName,
    string LastName
) : IRequest<Result<UserDto>>;
