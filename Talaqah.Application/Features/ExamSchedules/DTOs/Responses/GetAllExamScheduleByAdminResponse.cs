using System;
using System.Collections.Generic;
using System.Text;
using Talaqah.Application.Common.Pagination;
using Talaqah.Domain.Enums;

namespace Talaqah.Application.Features.ExamSchedule.DTOs.Responses
{
    public class ExamSchedulesViewByAdminResponse
    {
        public int TotalExams { get; set; }
        public int TotalActiveExams { get; set; }
        public int TotalExamsAttempted { get; set; }
        public required PagedResult<ExamScheduleDetailsByAdminResponse> ExamSchedules { get; set; }
    }

    public class ExamScheduleDetailsByAdminResponse
    {
        public int ExamScheduleId { get; set; }

        public string Title { get; set; } = string.Empty;

        public ExamScheduleTimeResponse? ExamScheduleDateTime { get; set; }
        public int DurationMinutes { get; set; }

        public required string ExamStatus { get; set; }

        public int TotalAttempts { get; set; }
    }
    public class ExamScheduleTimeResponse
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}
