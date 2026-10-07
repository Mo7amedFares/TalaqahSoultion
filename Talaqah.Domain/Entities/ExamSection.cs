using Talaqah.Domain.Enums;
using Talaqah.Domain.Common;

namespace Talaqah.Domain.Entities
{
    public class ExamSection : BaseEntity
    {

        public int ExamId { get; set; }
        public Exam Exam { get; set; } = null!;

        public string Title { get; set; } = string.Empty;

        public SkillType SkillType { get; set; }

        public int DisplayOrder { get; set; }

        public ICollection<ExamQuestionRule> ExamQuestionRules { get; set; }
            = new List<ExamQuestionRule>();
    }
}
