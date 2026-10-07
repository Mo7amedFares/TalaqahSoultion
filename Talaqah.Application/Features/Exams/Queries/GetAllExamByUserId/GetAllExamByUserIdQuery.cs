using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Pagination;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.Exams.DTOs;
using Talaqah.Application.Features.Exams.DTOs.Responses;

namespace Talaqah.Application.Features.Exams.Queries.GetAllExamByUserId;

public record GetAllExamByUserIdQuery(int Id, PaginationParameters Parameters)
    : IRequest<Result<PagedResult<ExamsViewByUserIdResponse>>>;
