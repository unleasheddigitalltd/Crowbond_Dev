using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Tasks.PutAway.GetPutAwayTaskAssignments;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.GetPutAwayTaskAssignmentLines;

internal sealed class GetPutAwayTaskAssignmentLinesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPutAwayTaskAssignmentLinesQuery, IReadOnlyCollection<TaskAssignmentLineResponse>>
{
    public async Task<Result<IReadOnlyCollection<TaskAssignmentLineResponse>>> Handle(GetPutAwayTaskAssignmentLinesQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                tl.id AS {nameof(TaskAssignmentLineResponse.TaskAssignmentLineId)},
                tl.product_id AS {nameof(TaskAssignmentLineResponse.ProductId)},
                p.sku AS {nameof(TaskAssignmentLineResponse.ProductSku)},
                p.name AS {nameof(TaskAssignmentLineResponse.ProductName)},
                p.unit_of_measure_name AS {nameof(TaskAssignmentLineResponse.UnitOfMeasure)},
                tl.requested_qty AS {nameof(TaskAssignmentLineResponse.RequestedQty)},
                tl.completed_qty AS {nameof(TaskAssignmentLineResponse.CompletedQty)},
                tl.missed_qty AS {nameof(TaskAssignmentLineResponse.MissedQty)},
                tl.status AS {nameof(TaskAssignmentLineResponse.Status)}
             FROM wms.task_assignment_lines tl
             INNER JOIN wms.products p ON tl.product_id = p.id
             INNER JOIN wms.task_assignments ta ON tl.task_assignment_id = ta.id
             INNER JOIN wms.task_headers t ON ta.task_header_id = t.id
             WHERE 
                t.id = @TaskHeaderId AND
                t.task_type = 0 AND
                ta.assigned_operator_id = @WarehouseOperatorId AND
                ta.status IN (0, 1, 2)
             """;

        List<TaskAssignmentLineResponse> taskAssignments = (await connection.QueryAsync<TaskAssignmentLineResponse>(sql, request)).AsList();

        return taskAssignments;
    }
}
