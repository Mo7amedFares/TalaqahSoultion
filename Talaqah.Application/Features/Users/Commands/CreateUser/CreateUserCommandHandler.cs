using Microsoft.EntityFrameworkCore;
using Talaqah.Application.Common.Interfaces;
using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.Users.DTOs;
using Talaqah.Application.Features.Users.Mapping;
using Talaqah.Domain.Entities;

namespace Talaqah.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler
    : IRequestHandler<CreateUserCommand, Result<UserDto>>
{
    private readonly IApplicationDbContext _context;

    public CreateUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<UserDto>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        User user = request.ToEntity();

        _context.Users.Add(user);

        await _context.SaveChangesAsync(cancellationToken);

        var dto = await _context.Users
            .AsNoTracking()
            .Where(u => u.Id == user.Id)
            .Select(UserMappings.ToDto)
            .FirstAsync(cancellationToken);

        return Result<UserDto>.Success(dto);
    }
}
