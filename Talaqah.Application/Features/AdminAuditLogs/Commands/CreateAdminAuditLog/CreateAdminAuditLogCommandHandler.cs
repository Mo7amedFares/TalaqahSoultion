using Microsoft.EntityFrameworkCore;
using Talaqah.Application.Common.Interfaces;
using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.AdminAuditLogs.DTOs;
using Talaqah.Application.Features.AdminAuditLogs.Mapping;

namespace Talaqah.Application.Features.AdminAuditLogs.Commands.CreateAdminAuditLog;

public class CreateAdminAuditLogCommandHandler
    : IRequestHandler<CreateAdminAuditLogCommand, Result<AdminAuditLogDto>>
{
    private readonly IApplicationDbContext _context;

    public CreateAdminAuditLogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<AdminAuditLogDto>> Handle(
        CreateAdminAuditLogCommand request,
        CancellationToken cancellationToken)
    {
        var auditLog = request.ToEntity();

        _context.AdminAuditLogs.Add(auditLog);

        await _context.SaveChangesAsync(cancellationToken);

        var dto = await _context.AdminAuditLogs
            .AsNoTracking()
            .Where(a => a.Id == auditLog.Id)
            .Select(AdminAuditLogMappings.ToDto)
            .FirstAsync(cancellationToken);

        return Result<AdminAuditLogDto>.Success(dto);
    }
}
