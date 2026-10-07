using Talaqah.Domain.Common;
using Talaqah.Domain.Enums;

namespace Talaqah.Domain.Entities
{
    public class ExamQuestionRule : BaseEntity
    {
        public int ExamSectionId { get; set; }
        public ExamSection ExamSection { get; set; } = null!;
        public CefrLevel CefrLevel { get; set; }
        public int NumberOfQuestions { get; set; }

    }
}
