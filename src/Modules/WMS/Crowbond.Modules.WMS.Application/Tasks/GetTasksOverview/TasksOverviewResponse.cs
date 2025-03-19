using Crowbond.Common.Application.Pagination;
using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Tasks.GetTasksOverview;

public sealed class TasksOverviewResponse(
    IReadOnlyCollection<TaskOverview> items,
    IPagination pagination)
    : PaginatedResponse<TaskOverview>(items, pagination);
