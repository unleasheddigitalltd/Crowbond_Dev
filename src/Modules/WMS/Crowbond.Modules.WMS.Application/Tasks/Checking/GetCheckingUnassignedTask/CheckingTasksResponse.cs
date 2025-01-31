using Crowbond.Common.Application.Pagination;

namespace Crowbond.Modules.WMS.Application.Tasks.Checking.GetCheckingUnassignedTask;

public sealed class CheckingTasksResponse : PaginatedResponse<CheckingTask>
{

    public CheckingTasksResponse(IReadOnlyCollection<CheckingTask> checkingTasks, IPagination pagination)
        : base(checkingTasks, pagination)
    { }
}

public sealed record CheckingTask
{
    public Guid Id { get; }
    public string TaskNo { get; }
    public string DispatchNo { get; }
    public Guid RouteTripId { get; }
    public string RouteName { get; }
    public DateOnly RouteTripDate { get; }
}
