using Talaqah.Domain.Common;

namespace Talaqah.Domain.Entities
{
    public class AdminAuditLog : BaseEntity
    {

        public int AdminId { get; set; }

        public virtual User Admin { get; set; } = null!;

        public string ActionType { get; set; } = string.Empty;

        public string TableName { get; set; } = string.Empty;

        public int? RecordId { get; set; }

        public string? OldValues { get; set; }

        public string? NewValues { get; set; }

        public string? IpAddress { get; set; }

        public DateTime Timestamp { get; set; }

    }
}