using Microsoft.EntityFrameworkCore;
using Talaqah.Application.Common.Interfaces;
using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.AdminAuditLogs.DTOs;
using Talaqah.Application.Features.AdminAuditLogs.Mapping;

namespace Talaqah.Application.Features.AdminAuditLogs.Queries.GetAdminAuditLogById;

public class GetAdminAuditLogByIdQueryHandler
    : IRequestHandler<GetAdminAuditLogByIdQuery, Result<AdminAuditLogDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAdminAuditLogByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<AdminAuditLogDto>> Handle(
        GetAdminAuditLogByIdQuery request,
        CancellationToken cancellationToken)
    {
        var log = await _context.AdminAuditLogs
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(AdminAuditLogMappings.ToDto)
            .FirstOrDefaultAsync(cancellationToken);

        return log is null
            ? Result<AdminAuditLogDto>.Failure($"Admin audit log with id {request.Id} was not found.")
            : Result<AdminAuditLogDto>.Success(log);
    }
}
