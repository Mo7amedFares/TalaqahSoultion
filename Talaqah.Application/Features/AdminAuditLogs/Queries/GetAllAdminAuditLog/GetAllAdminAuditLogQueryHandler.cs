using Microsoft.EntityFrameworkCore;
using Talaqah.Application.Common.Interfaces;
using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Pagination;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.AdminAuditLogs.DTOs;
using Talaqah.Application.Features.AdminAuditLogs.Mapping;

namespace Talaqah.Application.Features.AdminAuditLogs.Queries.GetAllAdminAuditLog;

public class GetAllAdminAuditLogQueryHandler
    : IRequestHandler<GetAllAdminAuditLogsQuery, Result<PagedResult<AdminAuditLogDto>>>
{
    private readonly IApplicationDbContext _context;

    public GetAllAdminAuditLogQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PagedResult<AdminAuditLogDto>>> Handle(
        GetAllAdminAuditLogsQuery request,
        CancellationToken cancellationToken)
    {
        var paged = await _context.AdminAuditLogs
            .AsNoTracking()
            .OrderByDescending(x => x.Timestamp)
            .Select(AdminAuditLogMappings.ToDto)
            .ToPagedResultAsync(request.Parameters, cancellationToken);

        return Result<PagedResult<AdminAuditLogDto>>.Success(paged);
    }
}
