namespace Talaqah.Application.Features.Exams.DTOs.Requests
{
    public record CreateExamScheduleRequest
    (
        int DurationInMinutes,
        DateTime StartDate,
        DateTime EndDate,
        TimeSpan StartTime,
        TimeSpan EndTime
    );
}
