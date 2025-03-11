using Crowbond.Common.Application.Pagination;
using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Tasks.GetTasksOverview;

public sealed record TasksOverviewResponse(IReadOnlyCollection<TaskOverview> Items, int TotalCount, int Page, int PageSize)
    : PaginatedResponse<TaskOverview>(Items, TotalCount, Page, PageSize);

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
