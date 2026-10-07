namespace Talaqah.Application.Features.Exams.DTOs.Responses;

public class ExamTimeResponse
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }
}