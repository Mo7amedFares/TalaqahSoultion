using Talaqah.Domain.Common;

namespace Talaqah.Domain.Entities
{
    public class UserExamPolicy : BaseEntity
    {
        public int UserId { get; set; }
        public int ExamId { get; set; }
        public int MaxAllowedAttempts { get; set; }
        public int AssignedByAdminId { get; set; }


        public virtual User User { get; set; } = null!;
        public virtual Exam Exam { get; set; } = null!;
        public virtual User AssignedByAdmin { get; set; } = null!;
    }
}
