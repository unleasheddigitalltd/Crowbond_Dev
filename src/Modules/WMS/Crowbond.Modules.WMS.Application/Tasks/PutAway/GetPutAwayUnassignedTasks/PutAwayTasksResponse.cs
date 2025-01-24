using Crowbond.Common.Application.Pagination;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.GetPutAwayUnassignedTasks;

public sealed class PutAwayTasksResponse : PaginatedResponse<PutAwayTask>
{

    public PutAwayTasksResponse(IReadOnlyCollection<PutAwayTask> putAwayTask, IPagination pagination)
        : base(putAwayTask, pagination)
    { }
}

public sealed record PutAwayTask
{
    public Guid Id { get; }
    public string TaskNo { get; }
    public string ReceiptNo { get; }
    public DateOnly ReceivedDate { get; }
    public long TotalItems { get; }
    public long RegisteredItems { get; }
}
