using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Features.Exams.DTOs.Responses;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Common.Pagination;

namespace Talaqah.Application.Features.Exams.Queries.GetAllExamByAdmin
{
    public record GetAllExamByAdminQuery(PaginationParameters Parameters) :
        IRequest<Result <ExamsViewByAdminResponse>>;
}
