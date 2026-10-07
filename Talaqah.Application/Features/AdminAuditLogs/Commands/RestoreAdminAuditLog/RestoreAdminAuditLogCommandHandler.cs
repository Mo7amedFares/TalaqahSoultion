using Microsoft.EntityFrameworkCore;
using Talaqah.Application.Common.Interfaces;
using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Results;

namespace Talaqah.Application.Features.AdminAuditLogs.Commands.RestoreAdminAuditLog;

public class RestoreAdminAuditLogCommandHandler
    : IRequestHandler<RestoreAdminAuditLogCommand, Result<bool>>
{
    private readonly IApplicationDbContext _context;

    public RestoreAdminAuditLogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(
        RestoreAdminAuditLogCommand request,
        CancellationToken cancellationToken)
    {
        var auditLog = await _context.AdminAuditLogs
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (auditLog is null)
            return Result<bool>.Failure($"Admin audit log with id {request.Id} was not found.");

        if (!auditLog.IsDeleted)
            return Result<bool>.Failure($"Admin audit log with id {request.Id} is not deleted.");

        auditLog.IsDeleted = false;
        auditLog.DeletedAt = null;
        auditLog.DeletedBy = null;

        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
