using Microsoft.EntityFrameworkCore;
using Talaqah.Application.Common.Interfaces;
using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Results;
using Talaqah.Domain.Entities;

namespace Talaqah.Application.Features.AiEvaluations.Commands.EvaluateStudentResponse;

public class EvaluateStudentResponseCommandHandler
    : IRequestHandler<EvaluateStudentResponseCommand, Result<bool>>
{
    private readonly IApplicationDbContext _context;
    private readonly IAiEvaluationService _aiEvaluationService;

    public EvaluateStudentResponseCommandHandler(
        IApplicationDbContext context,
        IAiEvaluationService aiEvaluationService)
    {
        _context = context;
        _aiEvaluationService = aiEvaluationService;
    }

    public async Task<Result<bool>> Handle(
        EvaluateStudentResponseCommand request,
        CancellationToken cancellationToken)
    {
        var studentResponse = await _context.StudentResponses
            .AsNoTracking()
            .Include(sr => sr.ExamAttemptQuestion)
                .ThenInclude(eatq => eatq.Question)
            .FirstOrDefaultAsync(sr => sr.Id == request.ResponseId, cancellationToken);

        if (studentResponse is null)
            return Result<bool>.Failure(
                $"Student response with id {request.ResponseId} was not found.");

        if (string.IsNullOrEmpty(studentResponse.TextResponse))
            return Result<bool>.Failure(
                $"Student response with id {request.ResponseId} has no text to evaluate.");

        var evaluation = await _aiEvaluationService
            .EvaluateStudentResponseAsync(studentResponse, cancellationToken);

        var existingEvaluation = await _context.AiEvaluations
            .FirstOrDefaultAsync(e => e.StudentResponseId == request.ResponseId, cancellationToken);

        if (existingEvaluation is not null)
        {
            existingEvaluation.ErrorAnalysis = evaluation.ErrorAnalysis;
            existingEvaluation.Recommendations = evaluation.Recommendations;
        }
        else
        {
            _context.AiEvaluations.Add(new AiEvaluation
            {
                StudentResponseId = request.ResponseId,
                ErrorAnalysis = evaluation.ErrorAnalysis,
                Recommendations = evaluation.Recommendations
            });
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
