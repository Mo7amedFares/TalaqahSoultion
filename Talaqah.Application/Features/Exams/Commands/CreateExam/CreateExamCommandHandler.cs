using Talaqah.Application.Common.Interfaces;
using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Results;
using Talaqah.Domain.Entities;
using Talaqah.Domain.Enums;


namespace Talaqah.Application.Features.Exams.Commands.CreateExam
{
    public class CreateExamCommandHandler : IRequestHandler<CreateExamCommand, Result <int> >
    {
        private readonly IApplicationDbContext _context;
        public CreateExamCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<int>> Handle(CreateExamCommand request, CancellationToken cancellationToken)
        {

            var exam = new Exam
            {
                Title = request.Title,
                DurationMinutes = request.CreateExamScheduleRequest.DurationInMinutes,
                ExamStatus = ExamStatus.Draft,
                CreatedAt = DateTime.UtcNow,

                ExamSchedules = new List<Talaqah.Domain.Entities.ExamSchedule>() {
                new (){
                        DurationMinutes = request.CreateExamScheduleRequest.DurationInMinutes,
                        StartDate = request.CreateExamScheduleRequest.StartDate,
                        EndDate = request.CreateExamScheduleRequest.EndDate,
                        StartTime = request.CreateExamScheduleRequest.StartTime,
                        EndTime = request.CreateExamScheduleRequest.EndTime
                    }
                },
                ExamSections = request.CreateExamSectionRequest.Select(section => new ExamSection
                {
                    Title = section.SectionTitle,
                    SkillType = section.SectionType,
                    DisplayOrder = section.DisplayOrder,

                    ExamQuestionRules = section.CreateExamQuestionRuleRequests
                    .Select(rule => new ExamQuestionRule
                    {
                     CefrLevel = rule.CefrLevel,
                     NumberOfQuestions = rule.NumberOfQuestions
                     }).ToList()

                }).ToList()

            };
                _context.Exams.Add(exam);
            await _context.SaveChangesAsync(cancellationToken);
            return Result<int>.Success(exam.Id);

        } 
    }
}
