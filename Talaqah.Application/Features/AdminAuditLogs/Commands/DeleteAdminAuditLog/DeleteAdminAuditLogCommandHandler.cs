using Microsoft.EntityFrameworkCore;
using Talaqah.Application.Common.Interfaces;
using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Results;

namespace Talaqah.Application.Features.AdminAuditLogs.Commands.DeleteAdminAuditLog;

public class DeleteAdminAuditLogCommandHandler
    : IRequestHandler<DeleteAdminAuditLogCommand, Result<bool>>
{
    private readonly IApplicationDbContext _context;

    public DeleteAdminAuditLogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(
        DeleteAdminAuditLogCommand request,
        CancellationToken cancellationToken)
    {
        var auditLog = await _context.AdminAuditLogs.FindAsync(
            new object[] { request.Id },
            cancellationToken);

        if (auditLog is null)
            return Result<bool>.Failure($"Admin audit log with id {request.Id} was not found.");

        _context.AdminAuditLogs.Remove(auditLog);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
