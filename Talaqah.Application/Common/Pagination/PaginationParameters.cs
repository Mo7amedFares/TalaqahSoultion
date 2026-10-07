namespace Talaqah.Application.Common.Pagination;

public sealed class PaginationParameters
{
    public const int MaxPageSize = 50;

    public int PageNumber { get; }

    public int PageSize { get; }

    public PaginationParameters(int pageNumber = 1, int pageSize = 10)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize is < 1 or > MaxPageSize ? 10 : pageSize;
    }
}
