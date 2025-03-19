using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Application.Pagination;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Tasks;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Tasks.GetTasksOverview;

internal sealed class GetTasksOverviewQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetTasksOverviewQuery, TasksOverviewResponse>
{
    public async Task<Result<TasksOverviewResponse>> Handle(
        GetTasksOverviewQuery request,
        CancellationToken cancellationToken)
    {
        await using var connection = await dbConnectionFactory.OpenConnectionAsync();

        var sortOrder = request.Order?.Equals("ASC", StringComparison.OrdinalIgnoreCase) == true ? "ASC" : "DESC";
        var orderByClause = request.Sort switch
        {
            "TaskNo" => "t.task_no",
            "DispatchNo" => "d.dispatch_no",
            "RouteName" => "d.route_name",
            "RouteTripDate" => "d.route_trip_date",
            "Status" => "COALESCE(ta.status, 0)",
            _ => "t.task_no" // Default sorting
        };

        var sql = $@"
            WITH FilteredTasks AS (
                SELECT
                    t.id AS {nameof(TaskOverview.TaskId)},
                    t.task_no AS {nameof(TaskOverview.TaskNo)},
                    d.dispatch_no AS {nameof(TaskOverview.DispatchNo)},
                    d.route_trip_id AS {nameof(TaskOverview.RouteTripId)},
                    d.route_name AS {nameof(TaskOverview.RouteName)},
                    d.route_trip_date AS {nameof(TaskOverview.RouteTripDate)},
                    t.task_type AS {nameof(TaskOverview.TaskType)},
                    COALESCE(ta.status, 0) AS {nameof(TaskOverview.Status)},
                    wo.full_name AS {nameof(TaskOverview.AssignedOperatorName)},
                    COUNT(dl.id) AS {nameof(TaskOverview.TotalLines)},
                    SUM(CASE 
                        WHEN t.task_type IN ({(int)TaskType.PickingItem}, {(int)TaskType.PickingBulk}) AND dl.is_picked = true THEN 1
                        WHEN t.task_type IN ({(int)TaskType.CheckingItem}, {(int)TaskType.CheckingBulk}) AND dl.is_checked = true THEN 1
                        ELSE 0 
                    END) AS {nameof(TaskOverview.CompletedLines)},
                    ROW_NUMBER() OVER (ORDER BY {orderByClause} {sortOrder}) AS RowNum
                FROM wms.task_headers t
                INNER JOIN wms.dispatch_headers d ON d.id = t.dispatch_id
                INNER JOIN wms.dispatch_lines dl ON d.id = dl.dispatch_header_id
                LEFT JOIN wms.task_assignments ta ON t.id = ta.task_header_id
                LEFT JOIN wms.warehouse_operators wo ON ta.warehouse_operator_id = wo.id
                WHERE (ta.status IS NULL OR ta.status IN ({(int)TaskAssignmentStatus.Pending}, {(int)TaskAssignmentStatus.InProgress}))
                GROUP BY t.id, t.task_no, d.dispatch_no, d.route_trip_id, d.route_name, d.route_trip_date, 
                         t.task_type, ta.status, wo.full_name
            )
            SELECT *
            FROM FilteredTasks
            WHERE RowNum BETWEEN @Offset + 1 AND @Offset + @PageSize;

            SELECT COUNT(*)
            FROM (
                SELECT t.id
                FROM wms.task_headers t
                LEFT JOIN wms.task_assignments ta ON t.id = ta.task_header_id
                WHERE (ta.status IS NULL OR ta.status IN ({(int)TaskAssignmentStatus.Pending}, {(int)TaskAssignmentStatus.InProgress}))
                GROUP BY t.id
            ) AS TaskCount;";

        using var multi = await connection.QueryMultipleAsync(
            sql,
            new
            {
                Offset = (request.Page - 1) * request.PageSize,
                request.PageSize
            });

        var tasks = await multi.ReadAsync<TaskOverview>();
        var totalCount = await multi.ReadSingleAsync<int>();

        var lastPage = (int)Math.Ceiling(totalCount / (double)request.PageSize);
        var pagination = new Pagination(totalCount, request.Page, request.PageSize, lastPage, request.PageSize, totalCount);
        return Result.Success(new TasksOverviewResponse(tasks.ToList(), pagination));
    }
}
