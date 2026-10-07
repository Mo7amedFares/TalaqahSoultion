using Talaqah.Domain.Common;

namespace Talaqah.Domain.Entities
{
    public class AntiCheatingLog : BaseEntity
    {
        public int AttemptId { get; set; }
        public string EventType { get; set; } = string.Empty;       // e.g., 'FullscreenExit', 'TabSwitch'
        public string Details { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public virtual ExamAttempt ExamAttempt { get; set; } = null!;
    }
}
