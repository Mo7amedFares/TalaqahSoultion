namespace Talaqah.Application.Common.Pagination;

public sealed class CursorPaginationParameters
{
    public int? Cursor { get; }

    public int PageSize { get; }

    public const int DefaultPageSize = 10;
    public const int MaxPageSize = 50;

    public CursorPaginationParameters(
        int? cursor = null,
        int pageSize = DefaultPageSize)
    {
        Cursor = cursor;

        PageSize = pageSize switch
        {
            < 1 => DefaultPageSize,
            > MaxPageSize => MaxPageSize,
            _ => pageSize
        };
    }
}
