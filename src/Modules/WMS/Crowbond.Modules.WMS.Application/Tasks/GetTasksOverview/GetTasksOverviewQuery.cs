using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Application.Pagination;

namespace Crowbond.Modules.WMS.Application.Tasks.GetTasksOverview;

public sealed record GetTasksOverviewQuery(
    string? Sort = null,
    string? Order = null,
    int Page = 1,
    int PageSize = 10) : IQuery<TasksOverviewResponse>;
