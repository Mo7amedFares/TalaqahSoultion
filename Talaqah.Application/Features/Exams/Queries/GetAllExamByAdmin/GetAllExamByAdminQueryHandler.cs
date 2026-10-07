using Microsoft.EntityFrameworkCore;
using Talaqah.Application.Common.Interfaces;
using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Pagination;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.Exams.DTOs.Responses;
using Talaqah.Domain.Enums;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Talaqah.Application.Features.Exams.Queries.GetAllExamByAdmin
{
 public class GetAllExamByAdminQueryHandler
     : IRequestHandler<GetAllExamByAdminQuery, Result<ExamsViewByAdminResponse>>
        {
            private readonly IApplicationDbContext _context;

            public GetAllExamByAdminQueryHandler(
                IApplicationDbContext context)
            {
                _context = context;
            }


            public async Task<Result<ExamsViewByAdminResponse>> Handle(
                GetAllExamByAdminQuery request,
                CancellationToken cancellationToken)
            {

                var query = _context.Exams
                    .AsNoTracking();

                var statistics = await query
                    .GroupBy(x => true)
                    .Select(g => new
                    {
                        Total = g.Count(),
                        Published = g.Count(x =>
                            x.ExamStatus == ExamStatus.Published),
                        Attempted = g.Count(x =>
                                         x.ExamSchedules
                                        .SelectMany(s => s.ExamAttempts)
                                        .Any())
                    })
                    .FirstOrDefaultAsync(cancellationToken);
                
            if (statistics == null) {return Result<ExamsViewByAdminResponse>.Failure("No statistics found");}


            var exams = await query
                  .OrderByDescending(e => e.CreatedAt)
                  .Select(e => new ExamDetailsByAdminResponse
                  {
                      ExamId = e.Id,                
                      Title = e.Title,
                      DurationMinutes = e.DurationMinutes,                  
                      ExamStatus = e.ExamStatus,
                  
                      LastExamSchedule =
                          e.ExamSchedules
                              .OrderByDescending(s => s.StartDate)
                              .ThenByDescending(s => s.StartTime)
                              .Select(s => new ExamTimeResponse
                              {
                                  StartDate = s.StartDate,
                                  EndDate = s.EndDate,
                                  StartTime = s.StartTime,
                                  EndTime = s.EndTime
                              })
                              .FirstOrDefault(),
                  
                      TotalQuestions =
                          e.ExamSections
                              .SelectMany(s => s.ExamQuestionRules)
                              .Sum(r => r.NumberOfQuestions),
                  
                      TotalAttempts =
                          e.ExamSchedules
                              .SelectMany(s => s.ExamAttempts)
                              .Count()
                  
                  })
                  .ToPagedResultAsync(request.Parameters, cancellationToken);



            return Result<ExamsViewByAdminResponse>.Success(
                    new ExamsViewByAdminResponse
                    {
                        TotalExams = statistics.Total,
                        TotalExamsPublished = statistics.Published,
                        ToatalExamsAttempted = statistics.Attempted,
                        Exams = exams
                    });
            }
        
 }
}

