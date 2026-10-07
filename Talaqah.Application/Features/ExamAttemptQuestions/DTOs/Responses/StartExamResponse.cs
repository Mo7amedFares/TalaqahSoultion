namespace Talaqah.Application.Features.ExamAttemptQuestions.DTOs.Responses;

public class StartExamResponse
{
    public int ExamAttemptId { get; set; }
    public string ExamTitle { get; set; } = null!;
    public IEnumerable<ExamSectionQuestionsDto> ExamSections { get; set; } = 
        new List<ExamSectionQuestionsDto>();
}

public class ExamSectionQuestionsDto
{
    public string ExamSectionTitle { get; set; } = null!;
    public int NumberOfQuestions { get; set; }
    public IEnumerable<QuestionDto> Questions { get; set; } = new List<QuestionDto>();
}

public class QuestionDto
{
    public int QuestionId { get; set; }
    public string QuestionText { get; set; } = null!;
    public int Order { get; set; }
    public string? QuestionVoiceUrl { get; set; }
    public IEnumerable<QuestionOptionDto>? QuestionOptions { get; set; }
                        = new List<QuestionOptionDto>();
}

public class QuestionOptionDto
{
    public int QuestionOptionId { get; set; }
    public string OptionText { get; set; } = null!;
}
