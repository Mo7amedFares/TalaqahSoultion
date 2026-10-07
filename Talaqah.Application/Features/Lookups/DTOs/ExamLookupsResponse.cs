namespace Talaqah.Application.Features.Lookups.DTOs;

public sealed class ExamLookupsResponse
{
    public IReadOnlyList<LookupResponse> SkillTypes { get; init; }
        = [];

    public IReadOnlyList<LookupResponse> CefrLevels { get; init; }
        = [];
}