using Talaqah.Domain.Enums;

namespace Talaqah.Application.Features.Exams.DTOs.Requests
{
    public record CreateExamSectionRequest
                    (    string SectionTitle, 
                         SkillType SectionType,
                         List<CreateExamQuestionRuleRequest> CreateExamQuestionRuleRequests,
                         int DisplayOrder
                         );
}

