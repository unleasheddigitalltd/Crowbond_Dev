using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Receipts;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetMyPickingTaskAssignmentLineDatails;

internal sealed class GetMyPickingTaskAssignmentLineDatailsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetMyPickingTaskAssignmentLineDatailsQuery, TaskAssignmentLineDetailsResponse>
{
    public async Task<Result<TaskAssignmentLineDetailsResponse>> Handle(GetMyPickingTaskAssignmentLineDatailsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 tl.id AS {nameof(TaskAssignmentLineDetailsResponse.Id)},
                 p.id AS {nameof(TaskAssignmentLineDetailsResponse.ProductId)},
                 p.name AS {nameof(TaskAssignmentLineDetailsResponse.ProductName)},
                 p.sku AS {nameof(TaskAssignmentLineDetailsResponse.ProductSku)},
                 tl.requested_qty AS {nameof(TaskAssignmentLineDetailsResponse.RequestedQty)},
                 tl.completed_qty AS {nameof(TaskAssignmentLineDetailsResponse.CompletedQty)}
             FROM wms.task_assignment_lines tl
             INNER JOIN wms.products p ON p.id = tl.product_id
             INNER JOIN wms.task_assignments ta ON ta.id = tl.task_assignment_id 
             WHERE 
                ta.assigned_operator_id = @WarehouseOperatorId 
                AND tl.id = @TaskAssignmentLineId
             """;

        TaskAssignmentLineDetailsResponse? taskAssignmentLine = await connection.QuerySingleOrDefaultAsync<TaskAssignmentLineDetailsResponse>(sql, request);

        if (taskAssignmentLine is null)
        {
            return Result.Failure<TaskAssignmentLineDetailsResponse>(ReceiptErrors.NotFound(request.TaskAssignmentLineId));
        }

        return taskAssignmentLine;
    }
}
