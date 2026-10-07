using Talaqah.Domain.Common;

namespace Talaqah.Domain.Entities
{
    public class ExamAttemptQuestion : BaseEntity
    {
         public int ExamAttemptId { get; set; }
         public virtual ExamAttempt ExamAttempt { get; set; } = null!;
         public int QuestionId { get; set; }
         public virtual Question Question { get; set; } = null!;
         public int Order { get; set; }
}
}       
