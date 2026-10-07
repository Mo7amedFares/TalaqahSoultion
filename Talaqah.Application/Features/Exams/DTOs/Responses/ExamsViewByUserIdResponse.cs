using System;
using System.Collections.Generic;
using System.Text;
using Talaqah.Domain.Enums;

namespace Talaqah.Application.Features.Exams.DTOs.Responses
{
    public class ExamsViewByUserIdResponse
    {
        public int ExamId { get; set; }   
        public string MainTitle { get; set; } = string.Empty; 
        public int DurationMinutes { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }   
        public TimeSpan StartTime { get; set; } 
        public TimeSpan EndTime { get; set; } 
        public int AttemptNumber { get; set; }
        public string Status { get; set; } = ExamAttemptStatus.NotStarted.ToString(); 
    }
}
