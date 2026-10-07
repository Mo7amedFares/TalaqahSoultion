namespace Talaqah.Application.Common.Pagination;

public sealed class CursorPagedResult<T>
{
    public IReadOnlyList<T> Items { get; }

    public string? NextCursor { get; }

    public bool HasNextPage { get; }

    private CursorPagedResult(
        IReadOnlyList<T> items,
        string? nextCursor,
        bool hasNextPage)
    {
        Items = items;
        NextCursor = nextCursor;
        HasNextPage = hasNextPage;
    }

    public static CursorPagedResult<T> Create(
        IReadOnlyList<T> items,
        string? nextCursor,
        bool hasNextPage)
    {
        return new(items, nextCursor, hasNextPage);
    }
}