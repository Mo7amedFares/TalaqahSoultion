using Microsoft.EntityFrameworkCore;
using Talaqah.Application.Common.Interfaces;
using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Results;

namespace Talaqah.Application.Features.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler
    : IRequestHandler<DeleteUserCommand, Result<bool>>
{
    private readonly IApplicationDbContext _context;

    public DeleteUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(
        DeleteUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(
            new object[] { request.Id },
            cancellationToken);

        if (user is null)
            return Result<bool>.Failure($"User with id {request.Id} was not found.");

        _context.Users.Remove(user);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
