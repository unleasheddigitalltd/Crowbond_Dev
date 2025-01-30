using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetPickingTaskDispatchLines;

internal sealed class GetPickingTaskDispatchLinesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPickingTaskDispatchLinesQuery, IReadOnlyCollection<TaskDispatchLineResponse>>
{
    public async Task<Result<IReadOnlyCollection<TaskDispatchLineResponse>>> Handle(GetPickingTaskDispatchLinesQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                dl.id AS {nameof(TaskDispatchLineResponse.DispatchLineId)},
                p.name AS {nameof(TaskDispatchLineResponse.ProductName)},
                dl.ordered_qty AS {nameof(TaskDispatchLineResponse.OrderedQty)},
                dl.picked_qty AS {nameof(TaskDispatchLineResponse.PickedQty)},
                dl.is_picked AS {nameof(TaskDispatchLineResponse.IsPicked)}
             FROM wms.task_headers t
             INNER JOIN wms.dispatch_headers d ON t.dispatch_id = d.id
             INNER JOIN wms.dispatch_lines dl ON dl.dispatch_header_id = d.id
             INNER JOIN wms.products p ON dl.product_id = p.id
             WHERE 
                t.task_type IN (1, 2) AND t.id = @TaskHeaderId
             """;

        List<TaskDispatchLineResponse> dispatchLines = (await connection.QueryAsync<TaskDispatchLineResponse>(sql, request)).AsList();

        return dispatchLines;
    }
}
