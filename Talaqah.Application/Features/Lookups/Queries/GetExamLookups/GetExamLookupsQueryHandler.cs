using Talaqah.Application.Common.Mediator;
using Talaqah.Application.Common.Results;
using Talaqah.Application.Features.Lookups.DTOs;
using Talaqah.Domain.Enums;

namespace Talaqah.Application.Features.Lookups.Queries.GetExamLookups;

public sealed class GetExamLookupsQueryHandler
    : IRequestHandler<GetExamLookupsQuery, Result<ExamLookupsResponse>>
{
    public Task<Result<ExamLookupsResponse>> Handle(
        GetExamLookupsQuery request,
        CancellationToken cancellationToken)
    {
        var response = new ExamLookupsResponse
        {
            SkillTypes = Enum.GetValues<SkillType>()
                .Select(x => new LookupResponse
                {
                    Id = (int)x,
                    Name = x.ToString()
                })
                .ToList(),

            CefrLevels = Enum.GetValues<CefrLevel>()
                .Select(x => new LookupResponse
                {
                    Id = (int)x,
                    Name = x.ToString()
                })
                .ToList()
        };

        return Task.FromResult(Result<ExamLookupsResponse>.Success(response));
    }
}