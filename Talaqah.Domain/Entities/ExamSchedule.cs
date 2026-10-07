using Talaqah.Domain.Common;

namespace Talaqah.Domain.Entities;

public class ExamSchedule : BaseEntity
{
    public int ExamId { get; set; }
    public virtual Exam Exam { get; set; } = null!;
    public int DurationMinutes { get; set; }
    public DateTime StartDate { get; set; } 
    public DateTime EndDate { get; set; }   
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }  
    public bool IsActive { get;  set; }
    public virtual ICollection<ExamAttempt> ExamAttempts { get; set; }
    = new List<ExamAttempt>();
}