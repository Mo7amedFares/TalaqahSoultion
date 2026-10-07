using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Results;

namespace Talaqah.Application.Features.Users.Commands.RestoreUser;

public record RestoreUserCommand(int Id)
    : IRequest<Result<bool>>;
