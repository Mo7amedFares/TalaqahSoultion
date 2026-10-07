using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Results;

namespace Talaqah.Application.Features.AdminAuditLogs.Commands.RestoreAdminAuditLog;

public record RestoreAdminAuditLogCommand(int Id)
    : IRequest<Result<bool>>;
