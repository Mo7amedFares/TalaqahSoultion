using System;
using System.Collections.Generic;
using System.Text;

namespace Talaqah.Application.Features.ExamAttemptQuestions.DTOs.Requests
{
    public class CreateExamAttemptQuestionRequest
    {
        public required int ExamAttemptId { get; set; }
        public required string ExamTitle { get; set; } = null!;
        public IEnumerable<GetOrderExamSectionQuestionDTO> ExamSections { get; set; } = 
            new List<GetOrderExamSectionQuestionDTO>();
    }
    public class GetOrderExamSectionQuestionDTO
    {
        public required string ExamSectionTitle { get; set; } = null!;
        public required int NumberOfQuestions { get; set; }
        public required IEnumerable<GetRandomQuestionDTO> RandomQuestions { get; set; } = new List<GetRandomQuestionDTO>();
    }
    public class GetRandomQuestionDTO
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; } = null!;
        public int Order { get; set; }
        public string? QuestionVoiceUrl { get; set; }
        public IEnumerable<GetQuestionOptionDTO>? QuestionOptions { get; set; }
                        = new List<GetQuestionOptionDTO>();
    }
    public class GetQuestionOptionDTO
    {
        public int QuestionOptionId { get; set; }
        public string OptionText { get; set; } = null!;
    }
}
