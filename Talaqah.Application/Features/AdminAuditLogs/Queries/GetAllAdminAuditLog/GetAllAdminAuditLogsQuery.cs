using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Pagination;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.AdminAuditLogs.DTOs;

namespace Talaqah.Application.Features.AdminAuditLogs.Queries.GetAllAdminAuditLog;

public record GetAllAdminAuditLogsQuery(PaginationParameters Parameters)
    : IRequest<Result<PagedResult<AdminAuditLogDto>>>;
