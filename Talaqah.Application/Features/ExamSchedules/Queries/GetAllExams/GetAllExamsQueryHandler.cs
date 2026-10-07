using Microsoft.EntityFrameworkCore;
using Talaqah.Application.Common.Interfaces;
using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Pagination;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.ExamSchedule.DTOs.Responses;

namespace Talaqah.Application.Features.ExamSchedule.Queries.GetAllExams
{
    public class GetAllExamsQueryHandler : IRequestHandler<GetAllExamsQuery, Result<ExamSchedulesViewByAdminResponse>>
    {
        private readonly IApplicationDbContext _context;
        public GetAllExamsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<ExamSchedulesViewByAdminResponse>> Handle(GetAllExamsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.ExamSchedules
                                .AsNoTracking()
                                .OrderByDescending(e => e.CreatedAt);

            var totalExams = await query.CountAsync(cancellationToken);
            var totalActiveExams = await query.CountAsync(e => e.IsActive, cancellationToken);
            var totalExamsAttempted = await query.CountAsync(e => e.ExamAttempts.Any(), cancellationToken);

            var pagedExams = await query
                .Select(e => new ExamScheduleDetailsByAdminResponse
                {
                    ExamScheduleId = e.Id,
                    Title = e.Exam.Title,
                    ExamScheduleDateTime = new ExamScheduleTimeResponse
                    {
                        StartDate = e.StartDate,
                        EndDate = e.EndDate,
                        StartTime = e.StartTime,
                        EndTime = e.EndTime
                    },
                    ExamStatus = e.Exam.ExamStatus.ToString(),
                    DurationMinutes = e.DurationMinutes,
                    TotalAttempts = e.ExamAttempts.Count()

                })
                .ToPagedResultAsync(request.Parameters, cancellationToken);

            return Result<ExamSchedulesViewByAdminResponse>.Success(new ExamSchedulesViewByAdminResponse
            {
                TotalExams = totalExams,
                TotalActiveExams = totalActiveExams,
                TotalExamsAttempted = totalExamsAttempted,
                ExamSchedules = pagedExams
            });

        }
    }
}
