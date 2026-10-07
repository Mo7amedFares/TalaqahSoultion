using Microsoft.EntityFrameworkCore;
using Talaqah.Application.Common.Interfaces;
using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.Users.DTOs;
using Talaqah.Application.Features.Users.Mapping;

namespace Talaqah.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler
    : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
{
    private readonly IApplicationDbContext _context;

    public GetUserByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<UserDto>> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(UserMappings.ToDto)
            .FirstOrDefaultAsync(cancellationToken);

        return user is null
            ? Result<UserDto>.Failure($"User with id {request.Id} was not found.")
            : Result<UserDto>.Success(user);
    }
}
