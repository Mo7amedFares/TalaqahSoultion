using System.Linq.Expressions;
using Talaqah.Application.Features.Exams.DTOs.Responses;
using Talaqah.Domain.Entities;

namespace Talaqah.Application.Features.Exams.Mappings;

public static class ExamMappings
{
    // Entity -> DTO
    // Used by EF Core Select()
    public static readonly Expression<Func<ExamAttempt, ExamsViewByUserIdResponse>> ToExamViewByUserIdDto =
        attempt => new ExamsViewByUserIdResponse
        {
            ExamId = attempt.ExamSchedule.Exam.Id,
            MainTitle = attempt.ExamSchedule.Exam.Title,
            DurationMinutes = attempt.ExamSchedule.Exam.DurationMinutes,

            StartDate = attempt.ExamSchedule.StartDate,
            EndDate = attempt.ExamSchedule.EndDate,
            StartTime = attempt.ExamSchedule.StartTime,
            EndTime = attempt.ExamSchedule.EndTime,

            AttemptNumber = attempt.AttemptNumber,
            Status = attempt.Status.ToString(),
        };
}