using Talaqah.Domain.Enums;
namespace Talaqah.Application.Features.Exams.DTOs.Responses;

public class ExamDetailsByAdminResponse
{
    public int ExamId { get; set; }

    public string Title { get; set; } = string.Empty;

    public ExamTimeResponse? LastExamSchedule { get; set; }
    public int DurationMinutes { get; set; }

    public required ExamStatus ExamStatus { get; set; }

    public int TotalQuestions { get; set; }

    public int TotalAttempts { get; set; }
}
