namespace Crowbond.Modules.WMS.Application.Stocks.GetStocks.Dtos;

public sealed record Pagination
{
    public int Length { get; private set; }
    public int Size { get; private set; }
    public int Page { get; private set; }
    public int LastPage { get; private set; }
    public int StartIndex { get; private set; }
    public int EndIndex { get; private set; }

    public Pagination(
        int length,
        int size,
        int page,
        int lastPage,
        int startIndex,
        int endIndex
        )
    {
        Length = length;
        Size = size;
        Page = page;
        LastPage = lastPage;
        StartIndex = startIndex;
        EndIndex = endIndex;
    }
}
