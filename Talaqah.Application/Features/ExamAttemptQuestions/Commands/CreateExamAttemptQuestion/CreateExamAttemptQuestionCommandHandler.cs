using Microsoft.EntityFrameworkCore;
using Talaqah.Application.Common.Interfaces;
using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.ExamAttemptQuestions.DTOs.Responses;
using Talaqah.Domain.Entities;
using Talaqah.Domain.Enums;


namespace Talaqah.Application.Features.ExamAttemptQuestions.Commands.CreateExamAttemptQuestion;

public class CreateExamAttemptQuestionCommandHandler
    : IRequestHandler<CreateExamAttemptQuestionCommand, Result<StartExamResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly Random _random = new();

    public CreateExamAttemptQuestionCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<Result<StartExamResponse>> Handle(
        CreateExamAttemptQuestionCommand request,
        CancellationToken cancellationToken)
    {
        await using var transaction =
            await _context.Database.BeginTransactionAsync(cancellationToken);


        try
        {
            var examAttempt = await _context.ExamAttempts
                .Include(x => x.ExamSchedule)
                    .ThenInclude(x => x.Exam)
                .FirstOrDefaultAsync(
                    x => x.Id == request.Request.ExamAttemptId,
                    cancellationToken);


            if (examAttempt is null)
            {
                return Result<StartExamResponse>
                    .Failure("Exam attempt not found.");
            }


            if (examAttempt.Status != ExamAttemptStatus.NotStarted)
            {
                return Result<StartExamResponse>
                    .Failure("Exam already started.");
            }

            var schedule = examAttempt.ExamSchedule;

            if (!schedule.IsActive)
            {
                return Result<StartExamResponse>
                    .Failure("Exam schedule is not active.");
            }

            var now = DateTime.UtcNow;
             if (now.Date < schedule.StartDate ||
                now.Date > schedule.EndDate.Date)
            {
                                    //examAttempt.Status = ExamAttemptStatus.Expired;
                                    //await _context.SaveChangesAsync(cancellationToken);
                return Result<StartExamResponse>
                    .Failure("Exam is not available now.");
            }

            var exists = await _context.ExamAttemptQuestions
                .AnyAsync(
                    x => x.ExamAttemptId == examAttempt.Id,
                    cancellationToken);


            if (exists)
            {
                return Result<StartExamResponse>
                    .Failure("Questions already generated.");
            }


            var sections = await _context.ExamSections
                .Where(x => x.ExamId == schedule.ExamId)
                .Include(x => x.ExamQuestionRules)
                .OrderBy(x => x.DisplayOrder)
                .AsNoTracking()
                .ToListAsync(cancellationToken);


            if (!sections.Any())
            {
                return Result<StartExamResponse>
                    .Failure("No exam sections found.");
            }

            // ===============================
            // Collect Required Rules
            // ===============================

            var requiredRules = sections
                .SelectMany(x => x.ExamQuestionRules
                    .Select(r => new
                    {
                        Section = x,
                        Rule = r
                    }))
                .ToList();

            var skillTypes = requiredRules
                .Select(x => x.Section.SkillType)
                .Distinct()
                .ToList();

            var cefrLevels = requiredRules
                .Select(x => x.Rule.CefrLevel)
                .Distinct()
                .ToList();

            // ===============================
            // Load Questions Once
            // ===============================

            var allQuestions = await _context.Questions
                .Where(q =>
                    skillTypes.Contains(q.SkillType) &&
                    cefrLevels.Contains(q.CefrLevel) &&
                    !q.IsDeleted)
                .Include(q =>
                    q.QuestionOptions
                        .Where(o => !o.IsDeleted))
                .AsNoTracking()
                .ToListAsync(cancellationToken);



            var usedQuestionIds = new HashSet<int>();
            var attemptQuestions = new List<ExamAttemptQuestion>();
            var sectionResponses = new List<ExamSectionQuestionsDto>();
            int order = 1;

            // ===============================
            // Generate Questions
            // ===============================

            foreach (var section in sections)
            {
                var sectionQuestions = new List<QuestionDto>();


                foreach (var rule in section.ExamQuestionRules
                    .OrderBy(x => x.CefrLevel))
                {

                    var availableQuestions = allQuestions
                        .Where(q =>
                            q.SkillType == section.SkillType &&
                            q.CefrLevel == rule.CefrLevel &&
                            !usedQuestionIds.Contains(q.Id))
                        .ToList();

                    if (availableQuestions.Count <
                        rule.NumberOfQuestions)
                    {
                        return Result<StartExamResponse>
                            .Failure(
                            $"Not enough {section.SkillType} " +
                            $"{rule.CefrLevel} questions.");
                    }

                    var selectedQuestions =
                        availableQuestions
                            .OrderBy(_ => _random.Next())
                            .Take(rule.NumberOfQuestions)
                            .ToList();


                    foreach (var question in selectedQuestions)
                    {

                        usedQuestionIds.Add(question.Id);
                        attemptQuestions.Add(
                            new ExamAttemptQuestion
                            {
                                ExamAttemptId = examAttempt.Id,
                                QuestionId = question.Id,
                                Order = order
                            });

                        sectionQuestions.Add(
                            new QuestionDto
                            {
                                QuestionId = question.Id,
                                QuestionText = question.QuestionText,
                                Order = order,
                                QuestionVoiceUrl = question.MediaUrl,

                                QuestionOptions =
                                    question.QuestionOptions
                                    .Select(o =>
                                        new QuestionOptionDto
                                        {
                                            QuestionOptionId = o.Id,
                                            OptionText = o.OptionText
                                        })
                                    .ToList()
                            });


                        order++;
                    }
                }

                sectionResponses.Add(
                    new ExamSectionQuestionsDto
                    {
                        ExamSectionTitle = section.Title,
                        NumberOfQuestions = sectionQuestions.Count,
                        Questions = sectionQuestions
                    });
            }



            // ===============================
            // Update Attempt
            // ===============================

            examAttempt.Status =
                ExamAttemptStatus.InProgress;

            examAttempt.StartedAt =
                DateTime.UtcNow;



            _context.ExamAttemptQuestions
                .AddRange(attemptQuestions);
            await _context.SaveChangesAsync(
                cancellationToken);
            await transaction.CommitAsync(
                cancellationToken);

            return Result<StartExamResponse>
                .Success(
                new StartExamResponse
                {
                    ExamAttemptId = examAttempt.Id,
                    ExamTitle =
                        examAttempt.ExamSchedule.Exam.Title,

                    ExamSections = sectionResponses
                });

        }
        catch
        {
            await transaction.RollbackAsync(
                cancellationToken);

            throw;
        }
    }
}