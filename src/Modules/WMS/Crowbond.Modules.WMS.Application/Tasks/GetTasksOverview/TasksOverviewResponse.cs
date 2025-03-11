using Crowbond.Common.Application.Pagination;
using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Tasks.GetTasksOverview;

public sealed class TasksOverviewResponse : PaginatedResponse<TaskOverview>
{
    public TasksOverviewResponse(
        IReadOnlyCollection<TaskOverview> items,
        IPagination pagination) : base(items, pagination)
    {
    }
}

public sealed record TaskOverview(
    Guid TaskId,
    string TaskNo,
    string DispatchNo,
    Guid RouteTripId,
    string RouteName,
    DateTime RouteTripDate,
    TaskType TaskType,
    TaskAssignmentStatus Status,
    string? AssignedOperatorName,
    long TotalLines,
    long CompletedLines);
