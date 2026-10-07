using Talaqah.Domain.Common;
using Talaqah.Domain.Enums;

namespace Talaqah.Domain.Entities;

public class Exam : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public ExamStatus ExamStatus { get; set; } = ExamStatus.Draft;
    public int? CreatedByAdminId { get; set; }
    public int? UpdatedByAdminId { get; set; }

    public virtual User? CreatedByAdmin { get; set; }
    public virtual User? UpdatedByAdmin { get; set; }
    public virtual ICollection<ExamSchedule> ExamSchedules { get; set; }
        = new List<ExamSchedule>();
 
    public virtual ICollection<UserExamPolicy> UserExamPolicies { get; set; }
        = new List<UserExamPolicy>();
    public virtual ICollection<ExamSection> ExamSections { get; set; }
        = new List<ExamSection>();
}