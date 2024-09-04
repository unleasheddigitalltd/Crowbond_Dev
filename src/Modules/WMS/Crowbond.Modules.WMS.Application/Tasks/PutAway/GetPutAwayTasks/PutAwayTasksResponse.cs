using Crowbond.Common.Application.Pagination;
using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.GetPutAwayTasks;

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
    public string ReceivedDate { get; }
    public string DeliveryNoteNumber { get; }
    public TaskHeaderStatus Status { get; }
}

