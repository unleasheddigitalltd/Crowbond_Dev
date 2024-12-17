using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.WarehouseOperators;
using Dapper;

namespace Crowbond.Modules.WMS.Application.WarehouseOperators.GetWarehouseOperatorActiveTaskAssignment;

internal sealed class GetWarehouseOperatorActiveTaskAssignmentQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetWarehouseOperatorActiveTaskAssignmentQuery, ActiveTaskAssignmentResponse>
{
    public async Task<Result<ActiveTaskAssignmentResponse>> Handle(
        GetWarehouseOperatorActiveTaskAssignmentQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 o.id AS {nameof(ActiveTaskAssignmentResponse.WarehouseOperatorId)},
                 t.id AS {nameof(ActiveTaskAssignmentResponse.TaskId)},
                 t.task_no AS {nameof(ActiveTaskAssignmentResponse.TaskNo)}
             FROM wms.warehouse_operators o
             LEFT JOIN wms.task_assignments ta 
                 ON o.id = ta.assigned_operator_id AND ta.status IN (0 , 1 , 2)
             LEFT JOIN wms.task_headers t 
                 ON t.id = ta.task_header_id
             WHERE o.id = @WarehouseOperatorId
             LIMIT 1;
             """;

        ActiveTaskAssignmentResponse? warehouseOperatorActivation = await connection.QuerySingleOrDefaultAsync<ActiveTaskAssignmentResponse>(sql, request);

        if (warehouseOperatorActivation is null)
        {
            return Result.Failure<ActiveTaskAssignmentResponse>(WarehouseOperatorErrors.NotFound(request.WarehouseOperatorId));
        }

        return warehouseOperatorActivation;
    }
}
