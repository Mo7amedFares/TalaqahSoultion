using Microsoft.EntityFrameworkCore;

namespace Talaqah.Application.Common.Pagination;

public static class PaginationExtensions
{
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> source,
        PaginationParameters parameters,
        CancellationToken cancellationToken = default)
    {
        var totalCount = await source.CountAsync(cancellationToken);

        var items = await source
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .ToListAsync(cancellationToken);

        return PagedResult<T>.Create(items, parameters.PageNumber, parameters.PageSize, totalCount);
    }
}
