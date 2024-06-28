namespace Crowbond.Common.Application.Pagination;
public class Pagination : IPagination
{
    public Pagination(int length, int size, int page, int lastPage, int startIndex, int endIndex)
    {
        Length = length;
        Size = size;
        Page = page;
        LastPage = lastPage;
        StartIndex = startIndex;
        EndIndex = endIndex;
    }

    public int Length { get; init; }

    public int Size { get; init; }

    public int Page { get; init; }

    public int LastPage { get; init; }

    public int StartIndex { get; init; }

    public int EndIndex { get; init; }
}
