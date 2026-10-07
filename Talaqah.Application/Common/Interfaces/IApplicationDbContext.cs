using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Talaqah.Domain.Entities;

namespace Talaqah.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DatabaseFacade Database { get; }

    DbSet<User> Users { get; }
    DbSet<College> Colleges { get; }
    DbSet<School> Schools { get; }

    DbSet<Question> Questions { get; }
    DbSet<QuestionOption> QuestionOptions { get; }

    DbSet<Exam> Exams { get; }
    DbSet<UserExamPolicy> UserExamPolicies { get; }

    DbSet<ExamAttempt> ExamAttempts { get; }
    DbSet<StudentResponse> StudentResponses { get; }

    DbSet<AiEvaluation> AiEvaluations { get; }
    DbSet<AntiCheatingLog> AntiCheatingLogs { get; }
    DbSet<AdminAuditLog> AdminAuditLogs { get; }
    DbSet<Certificate> Certificates { get; }
    DbSet<ExamSchedule> ExamSchedules { get; }
    DbSet<ExamAttemptQuestion> ExamAttemptQuestions { get; }
    DbSet<ExamQuestionRule> ExamQuestionRules { get; }
    DbSet<ExamSection> ExamSections { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
