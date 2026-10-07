using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Pagination;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.ExamSchedule.DTOs.Responses;
using Talaqah.Domain.Enums;

namespace Talaqah.Application.Features.ExamSchedule.Queries.GetAllExams
{
    public record GetAllExamsQuery(PaginationParameters Parameters)
                : IRequest<Result<ExamSchedulesViewByAdminResponse>>;
}
