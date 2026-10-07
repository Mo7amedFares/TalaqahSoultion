using Microsoft.EntityFrameworkCore;
using Talaqah.Application.Common.Interfaces;
using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.Users.DTOs;
using Talaqah.Application.Features.Users.Mapping;

namespace Talaqah.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler
    : IRequestHandler<UpdateUserCommand, Result<UserDto>>
{
    private readonly IApplicationDbContext _context;

    public UpdateUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<UserDto>> Handle(
        UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == request.Id,
                cancellationToken);

        if (user is null)
            return Result<UserDto>.Failure($"User with id {request.Id} was not found.");

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;

        await _context.SaveChangesAsync(cancellationToken);

        var dto = await _context.Users
            .AsNoTracking()
            .Where(u => u.Id == request.Id)
            .Select(UserMappings.ToDto)
            .FirstAsync(cancellationToken);

        return Result<UserDto>.Success(dto);
    }
}
