﻿using Crowbond.Common.Application.Pagination;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetPickingTasksUnassigned;

public sealed class PickingTasksResponse : PaginatedResponse<PickingTask>
{

    public PickingTasksResponse(IReadOnlyCollection<PickingTask> pikingTask, IPagination pagination)
        : base(pikingTask, pagination)
    { }
}

public sealed record PickingTask
{
    public Guid Id { get; }
    public string TaskNo { get; }
    public string DispatchNo { get; }
    public Guid RouteTripId { get; }
    public string RouteName { get; }
    public DateOnly RouteTripDate { get; }
}
