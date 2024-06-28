namespace Crowbond.Common.Application.Pagination;

public interface IPagination
{
    int Length { get; }
    int Size { get; }
    int Page { get; }
    int LastPage { get; }
    int StartIndex { get; }
    int EndIndex { get; }
}
