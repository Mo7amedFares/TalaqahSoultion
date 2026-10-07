using Talaqah.Domain.Common;

namespace Talaqah.Domain.Entities
{
    public class QuestionOption : BaseEntity
    {
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; } = null!;
        public string OptionText { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}
