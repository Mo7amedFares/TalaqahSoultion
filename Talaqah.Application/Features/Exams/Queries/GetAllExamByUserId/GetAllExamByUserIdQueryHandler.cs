using Microsoft.EntityFrameworkCore;
using Talaqah.Application.Common.Interfaces;
using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Pagination;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.Exams.DTOs;
using Talaqah.Application.Features.Exams.DTOs.Responses;
using Talaqah.Application.Features.Exams.Mappings;

namespace Talaqah.Application.Features.Exams.Queries.GetAllExamByUserId;

public class GetAllExamByUserIdQueryHandler
    : IRequestHandler<GetAllExamByUserIdQuery, Result<PagedResult<ExamsViewByUserIdResponse>>>
{
    private readonly IApplicationDbContext _context;

    public GetAllExamByUserIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PagedResult<ExamsViewByUserIdResponse>>> Handle(
        GetAllExamByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var paged = await _context.ExamAttempts
            .AsNoTracking()
            .Where(x => x.StudentId == request.Id)
            .Select(ExamMappings.ToExamViewByUserIdDto)
            .ToPagedResultAsync(request.Parameters, cancellationToken);

        return Result<PagedResult<ExamsViewByUserIdResponse>>.Success(paged);
    }
}
