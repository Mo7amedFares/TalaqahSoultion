using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Talaqah.Application.Common.Interfaces;
using Talaqah.Domain.Common;
using Talaqah.Domain.Entities;

namespace Talaqah.Persistence.Contexts;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly ICurrentUserService _currentUserService;

    public override DatabaseFacade Database =>  base.Database;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ICurrentUserService currentUserService)
        : base(options)
    {
        _currentUserService = currentUserService;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Best Practice: Scans this project assembly and applies
        // all classes implementing IEntityTypeConfiguration<T> automatically.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    // Automatic audit + soft delete:
    //   - Added   -> stamp CreatedAt / CreatedBy
    //   - Modified-> stamp UpdatedAt / UpdatedBy
    //   - Deleted -> convert to soft delete (IsDeleted, DeletedAt, DeletedBy)
    // Cascaded dependents that are marked Deleted are also soft-deleted transparently.
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var userId = _currentUserService.UserId;

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy = userId;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = userId;
                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.DeletedAt = DateTime.UtcNow;
                    entry.Entity.DeletedBy = userId;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    // Subsystem A: Identity & Institutional
    public DbSet<User> Users => Set<User>();
    public DbSet<College> Colleges => Set<College>();
    public DbSet<School> Schools => Set<School>();

    // Subsystem B: Manual Question Bank
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<QuestionOption> QuestionOptions => Set<QuestionOption>();

    // Subsystem C: Exam Configs & Policies
    public DbSet<Exam> Exams => Set<Exam>();
    public DbSet<UserExamPolicy> UserExamPolicies => Set<UserExamPolicy>();

    // Subsystem D: Live Tracking & Responses
    public DbSet<ExamAttempt> ExamAttempts => Set<ExamAttempt>();
    public DbSet<ExamSchedule> ExamSchedules => Set<ExamSchedule>();
    public DbSet<ExamAttemptQuestion> ExamAttemptQuestions => Set<ExamAttemptQuestion>();
    public DbSet<ExamQuestionRule> ExamQuestionRules => Set<ExamQuestionRule>();
    public DbSet<ExamSection> ExamSections => Set<ExamSection>();
    public DbSet<StudentResponse> StudentResponses => Set<StudentResponse>();

    // Subsystem E: AI Loop, Certificates & Security Telemetry
    public DbSet<AiEvaluation> AiEvaluations => Set<AiEvaluation>();
    public DbSet<AntiCheatingLog> AntiCheatingLogs => Set<AntiCheatingLog>();
    public DbSet<AdminAuditLog> AdminAuditLogs => Set<AdminAuditLog>();
    public DbSet<Certificate> Certificates => Set<Certificate>();
}
