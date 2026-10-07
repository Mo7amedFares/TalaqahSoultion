using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.Users.DTOs;

namespace Talaqah.Application.Features.Users.Queries.GetUserById;

public record GetUserByIdQuery(int Id)
    : IRequest<Result<UserDto>>;
