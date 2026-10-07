using System;
using System.Collections.Generic;
using System.Text;

namespace Talaqah.Application.Features.ExamAttempts.Commands.CreateExamAttempt
{
    public record CreateExamAttemptCommand(
        int ExamScheduleId,
        int StudentId
    );
}
