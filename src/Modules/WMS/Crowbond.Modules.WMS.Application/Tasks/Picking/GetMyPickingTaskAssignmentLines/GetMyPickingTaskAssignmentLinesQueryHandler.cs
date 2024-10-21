using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetMyPickingTaskAssignmentLines;

internal sealed class GetMyPickingTaskAssignmentLinesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetMyPickingTaskAssignmentLinesQuery, IReadOnlyCollection<TaskAssignmentLineResponse>>
{
    public async Task<Result<IReadOnlyCollection<TaskAssignmentLineResponse>>> Handle(GetMyPickingTaskAssignmentLinesQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                tl.id AS {nameof(TaskAssignmentLineResponse.Id)},
                t.task_no AS {nameof(TaskAssignmentLineResponse.TaskNo)},
                tl.product_id AS {nameof(TaskAssignmentLineResponse.ProductId)},
                p.sku AS {nameof(TaskAssignmentLineResponse.ProductSku)},
                p.name AS {nameof(TaskAssignmentLineResponse.ProductName)},
                p.unit_of_measure_name AS {nameof(TaskAssignmentLineResponse.UnitOfMeasure)},
                tl.requested_qty AS {nameof(TaskAssignmentLineResponse.RequestedQty)},
                tl.completed_qty AS {nameof(TaskAssignmentLineResponse.CompletedQty)},
                tl.status AS {nameof(TaskAssignmentLineResponse.Status)}
             FROM wms.task_assignment_lines tl
             INNER JOIN wms.products p ON tl.product_id = p.id
             INNER JOIN wms.task_assignments ta ON tl.task_assignment_id = ta.id
             INNER JOIN wms.task_headers t ON ta.task_header_id = t.id
             WHERE 
                t.id = @TaskHeaderId AND
                t.task_type = 1 AND
                ta.assigned_operator_id = @WarehouseOperatorId AND
                ta.status IN (0, 1, 2)
             """;

        List<TaskAssignmentLineResponse> taskAssignments = (await connection.QueryAsync<TaskAssignmentLineResponse>(sql, request)).AsList();

        return taskAssignments;
    }
}
