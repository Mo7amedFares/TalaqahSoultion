using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.AdminAuditLogs.DTOs;
using Talaqah.Domain.Enums;

namespace Talaqah.Application.Features.AdminAuditLogs.Commands.CreateAdminAuditLog;

public record CreateAdminAuditLogCommand(
    int AdminId,
    AuditActionType ActionType,
    string TableName,
    int? RecordId,
    string? OldValues,
    string? NewValues
) : IRequest<Result<AdminAuditLogDto>>;
