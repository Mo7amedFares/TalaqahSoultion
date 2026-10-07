using Microsoft.EntityFrameworkCore;

namespace Talaqah.Application.Common.Pagination;
public static class CursorPaginationExtensions
{
    public static async Task<CursorPagedResult<T>>
            ToCursorPagedResultAsync<T>(
             this IQueryable<T> query,
             int pageSize,
             Func<T, int> cursorSelector,
             CancellationToken cancellationToken = default)
        where T : class
    {
        var items = await query
            .Take(pageSize + 1)
            .ToListAsync(cancellationToken);

        var hasNext = items.Count > pageSize;

        if (hasNext)
            items.RemoveAt(pageSize);

        var nextCursor = hasNext
            ? cursorSelector(items.Last()).ToString()
            : null;

        return CursorPagedResult<T>.Create(
            items,
            nextCursor,
            hasNext);
    }
}