using System;
using System.Collections.Generic;
using System.Text;
using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.Exams.DTOs.Requests;

namespace Talaqah.Application.Features.Exams.Commands.CreateExam
{
    public record CreateExamCommand
                    (
                        string Title, 
                        CreateExamScheduleRequest CreateExamScheduleRequest,
                        List<CreateExamSectionRequest> CreateExamSectionRequest

                    ) : IRequest<Result<int>>;
}

