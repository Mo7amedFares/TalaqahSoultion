using Talaqah.Domain.Common;

namespace Talaqah.Domain.Entities
{
    public class StudentResponse : BaseEntity
    {
        public int ExamAttemptQuestionId { get; set; }
        public virtual ExamAttemptQuestion ExamAttemptQuestion { get; set; }
            = null!;
        public int? SelectedOptionId { get; set; }
        public virtual QuestionOption? SelectedOption { get; set; }
        public string? TextResponse { get; set; }
        public string? AudioResponseUrl { get; set; }
        public bool? IsCorrect { get; set; }
        public decimal? Score { get; set; }
        public int? ExamAttemptId { get; set; }
        public ExamAttempt? ExamAttempt { get; set; }
        public virtual AiEvaluation? AiEvaluation { get; set; }
    }
}
