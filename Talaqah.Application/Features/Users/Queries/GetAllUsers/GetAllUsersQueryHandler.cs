using Microsoft.EntityFrameworkCore;
using Talaqah.Application.Common.Interfaces;
using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Pagination;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.Users.DTOs;
using Talaqah.Application.Features.Users.Mapping;

namespace Talaqah.Application.Features.Users.Queries.GetAllUsers;

public class GetAllUsersQueryHandler
    : IRequestHandler<GetAllUsersQuery, Result<PagedResult<UserDto>>>
{
    private readonly IApplicationDbContext _context;

    public GetAllUsersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PagedResult<UserDto>>> Handle(
        GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        var paged = await _context.Users
            .AsNoTracking()
            .Select(UserMappings.ToDto)
            .ToPagedResultAsync(request.Parameters, cancellationToken);

        return Result<PagedResult<UserDto>>.Success(paged);
    }
}
