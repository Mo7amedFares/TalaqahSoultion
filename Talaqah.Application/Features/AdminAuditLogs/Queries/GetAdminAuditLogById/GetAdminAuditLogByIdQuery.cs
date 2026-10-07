using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.AdminAuditLogs.DTOs;

namespace Talaqah.Application.Features.AdminAuditLogs.Queries.GetAdminAuditLogById;

public record GetAdminAuditLogByIdQuery(int Id)
    : IRequest<Result<AdminAuditLogDto>>;
