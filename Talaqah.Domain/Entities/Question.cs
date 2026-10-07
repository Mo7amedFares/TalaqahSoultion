using Talaqah.Domain.Common;
using Talaqah.Domain.Enums;

namespace Talaqah.Domain.Entities
{
    public class Question : BaseEntity
    {
        public SkillType SkillType { get; set; }
        public CefrLevel CefrLevel { get; set; }
        public QuestionType QuestionType { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string? MediaUrl { get; set; }
        public virtual ICollection<QuestionOption> QuestionOptions { get; set; }
            = new List<QuestionOption>();
    }
}
