using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Pagination;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.Users.DTOs;

namespace Talaqah.Application.Features.Users.Queries.GetAllUsers;

public record GetAllUsersQuery(PaginationParameters Parameters)
    : IRequest<Result<PagedResult<UserDto>>>;
