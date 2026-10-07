using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.Lookups.DTOs;

namespace Talaqah.Application.Features.Lookups.Queries.GetExamLookups;

public sealed record GetExamLookupsQuery
    : IRequest<Result<ExamLookupsResponse>>;