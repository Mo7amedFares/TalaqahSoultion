using System.Linq.Expressions;
using Talaqah.Application.Features.AdminAuditLogs.Commands.CreateAdminAuditLog;
using Talaqah.Application.Features.AdminAuditLogs.DTOs;
using Talaqah.Domain.Entities;

namespace Talaqah.Application.Features.AdminAuditLogs.Mapping;

public static class AdminAuditLogMappings
{
    // Entity -> DTO
    // Used for Query projection
    public static readonly Expression<Func<AdminAuditLog, AdminAuditLogDto>> ToDto =
        log => new AdminAuditLogDto
        {
            Id = log.Id,

            AdminId = log.AdminId,
            AdminName = log.Admin.FirstName + " " + log.Admin.LastName,

            ActionType = log.ActionType.ToString(),

            TableName = log.TableName,

            RecordId = log.RecordId,

            OldValues = log.OldValues,

            NewValues = log.NewValues,

            IpAddress = log.IpAddress,

            Timestamp = log.Timestamp
        };

    // Command -> Entity
    // Used in CreateAdminAuditLogCommandHandler
    public static AdminAuditLog ToEntity(
        this CreateAdminAuditLogCommand command)
    {
        return new AdminAuditLog
        {
            AdminId = command.AdminId,
            ActionType = Enum.GetName(command.ActionType)?? string.Empty,
            TableName = command.TableName,
            RecordId = command.RecordId,
            OldValues = command.OldValues,
            NewValues = command.NewValues,

            // Generated automatically
            Timestamp = DateTime.UtcNow
        };
    }
}
