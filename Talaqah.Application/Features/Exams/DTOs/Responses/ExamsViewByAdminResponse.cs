using Talaqah.Application.Common.Pagination;
namespace Talaqah.Application.Features.Exams.DTOs.Responses
{
    public class ExamsViewByAdminResponse
    {
        public int TotalExams { get; set; }
        public int TotalExamsPublished { get; set; }
        public int ToatalExamsAttempted { get; set; }

        public required PagedResult<ExamDetailsByAdminResponse> Exams { get; set; }

    };
}
