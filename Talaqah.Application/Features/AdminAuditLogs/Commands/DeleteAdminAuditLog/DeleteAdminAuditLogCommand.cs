using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Results;

namespace Talaqah.Application.Features.AdminAuditLogs.Commands.DeleteAdminAuditLog;

public record DeleteAdminAuditLogCommand(int Id)
    : IRequest<Result<bool>>;
