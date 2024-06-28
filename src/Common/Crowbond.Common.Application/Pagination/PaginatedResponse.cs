namespace Crowbond.Common.Application.Pagination;

public abstract class PaginatedResponse<T>
{
    public IReadOnlyCollection<T> Items { get; }
    public IPagination Pagination { get; }

    protected PaginatedResponse(IReadOnlyCollection<T> items, IPagination pagination)
    {
        Items = items;
        Pagination = pagination;
    }
}
