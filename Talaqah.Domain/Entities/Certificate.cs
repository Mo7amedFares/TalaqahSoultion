using Talaqah.Domain.Common;

namespace Talaqah.Domain.Entities
{
    public class Certificate : BaseEntity
    {
        public int AttemptId { get; set; }
        public int StudentId { get; set; }
        public string CertificateCode { get; set; } = string.Empty;
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
        public virtual ExamAttempt ExamAttempt { get; set; } = null!;
        public virtual User Student { get; set; } = null!;
    }
}
