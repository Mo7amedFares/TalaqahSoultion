using Talaqah.Domain.Common;
using Talaqah.Domain.Enums;

namespace Talaqah.Domain.Entities;

public class ExamAttempt : BaseEntity
{
    public required int ExamScheduleId { get; set; }
    public required int StudentId { get; set; }
    public required int AttemptNumber { get; set; } 
    public DateTime? StartedAt { get; set; }    
    public DateTime? SubmittedAt { get; set; } 
    public decimal? FinalScore { get; set; } 
    public CefrLevel? FinalCefrLevel { get; set; } = null;
    public ExamAttemptStatus Status { get; set; } = ExamAttemptStatus.NotStarted; 
    public virtual ExamSchedule ExamSchedule { get; set; } = null!;
    public virtual User Student { get; set; } = null!;
    public virtual ICollection<ExamAttemptQuestion> ExamAttemptQuestions { get; set; }
        = new List<ExamAttemptQuestion>();
    public virtual ICollection<StudentResponse> StudentResponses { get; set; }
        = new List<StudentResponse>();
}