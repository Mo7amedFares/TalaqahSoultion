using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Results;

namespace Talaqah.Application.Features.Users.Commands.DeleteUser;

public record DeleteUserCommand(int Id)
    : IRequest<Result<bool>>;
