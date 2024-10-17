using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Application.Pagination;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetPickingTasksUnassigned;

internal sealed class GetPickingTasksUnassignedQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPickingTasksUnassignedQuery, PickingTasksResponse>
{
    public async Task<Result<PickingTasksResponse>> Handle(GetPickingTasksUnassignedQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string sortOrder = request.Order.Equals("ASC", StringComparison.OrdinalIgnoreCase) ? "ASC" : "DESC";
        string orderByClause = request.Sort switch
        {
            "TaskNo" => "t.task_no",
            "DispatchNo" => "d.dispatch_no",
            "OrderNo" => "d.order_no",
            _ => "t.task_no" // Default sorting
        };

        string sql = $@"
            WITH FilteredPutAwayTasks AS (
                SELECT
                    t.id AS {nameof(PickingTask.Id)},
                    t.task_no AS {nameof(PickingTask.TaskNo)},
                    d.dispatch_no AS {nameof(PickingTask.DispatchNo)},
                    d.order_no AS {nameof(PickingTask.OrderNo)},
                    ROW_NUMBER() OVER (ORDER BY {orderByClause} {sortOrder}) AS RowNum
                FROM wms.task_headers t
                LEFT JOIN wms.task_assignments ta ON ta.task_header_id = t.id
                INNER JOIN wms.dispatch_headers d ON d.id = t.dispatch_id
                WHERE
                    t.task_type = 1 
                    AND ta.id IS NULL
                    AND (
                        t.task_no ILIKE '%' || @Search || '%'
                        OR d.dispatch_no ILIKE '%' || @Search || '%'   
                        OR d.order_no ILIKE '%' || @Search || '%'
                    )
            )
            SELECT 
                t.{nameof(PickingTask.Id)},
                t.{nameof(PickingTask.TaskNo)},
                t.{nameof(PickingTask.DispatchNo)},
                t.{nameof(PickingTask.OrderNo)}
            FROM FilteredPutAwayTasks t
            WHERE t.RowNum BETWEEN ((@Page) * @Size) + 1 AND (@Page + 1) * @Size
            ORDER BY t.RowNum;

            SELECT Count(*) 
            FROM wms.task_headers t
            LEFT JOIN wms.task_assignments ta ON ta.task_header_id = t.id
            INNER JOIN wms.dispatch_headers d ON d.id = t.dispatch_id
            WHERE
                t.task_type = 1 
                AND ta.id IS NULL
                AND (
                    t.task_no ILIKE '%' || @Search || '%'
                    OR d.dispatch_no ILIKE '%' || @Search || '%'   
                    OR d.order_no ILIKE '%' || @Search || '%'
                )                    
        ";

        SqlMapper.GridReader multi = await connection.QueryMultipleAsync(sql, request);

        var pickingTasks = (await multi.ReadAsync<PickingTask>()).ToList();
        int totalCount = await multi.ReadSingleAsync<int>();

        int totalPages = (int)Math.Ceiling(totalCount / (double)request.Size);
        int currentPage = request.Page;
        int pageSize = request.Size;
        int startIndex = currentPage * pageSize;
        int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);

        return new PickingTasksResponse(pickingTasks, new Pagination(totalCount, pageSize, currentPage, totalPages, startIndex, endIndex));
    }
}
