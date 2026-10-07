using Microsoft.EntityFrameworkCore;
using Talaqah.Application.Common.Interfaces;
using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Results;

namespace Talaqah.Application.Features.Users.Commands.RestoreUser;

public class RestoreUserCommandHandler
    : IRequestHandler<RestoreUserCommand, Result<bool>>
{
    private readonly IApplicationDbContext _context;

    public RestoreUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(
        RestoreUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (user is null)
            return Result<bool>.Failure($"User with id {request.Id} was not found.");

        if (!user.IsDeleted)
            return Result<bool>.Failure($"User with id {request.Id} is not deleted.");

        user.IsDeleted = false;
        user.DeletedAt = null;
        user.DeletedBy = null;

        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
