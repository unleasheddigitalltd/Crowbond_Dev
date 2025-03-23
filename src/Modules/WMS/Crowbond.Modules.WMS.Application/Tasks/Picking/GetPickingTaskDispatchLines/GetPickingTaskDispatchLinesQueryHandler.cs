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
        await using var connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                dl.id AS {nameof(TaskDispatchLineResponse.DispatchLineId)},
                p.id AS {nameof(TaskDispatchLineResponse.ProductId)},
                p.name AS {nameof(TaskDispatchLineResponse.ProductName)},
                p.unit_of_measure_name AS {nameof(TaskDispatchLineResponse.UnitOfMeasure)},
                dl.ordered_qty AS {nameof(TaskDispatchLineResponse.OrderedQty)},
                dl.picked_qty AS {nameof(TaskDispatchLineResponse.PickedQty)},
                dl.is_picked AS {nameof(TaskDispatchLineResponse.IsPicked)},
                COALESCE(l.name, 'No Location') AS {nameof(TaskDispatchLineResponse.Location)}
             FROM wms.task_headers t
             INNER JOIN wms.dispatch_headers d ON t.dispatch_id = d.id
             INNER JOIN wms.dispatch_lines dl ON dl.dispatch_header_id = d.id
             INNER JOIN wms.products p ON dl.product_id = p.id
             LEFT JOIN wms.stocks s ON s.product_id = dl.product_id
             LEFT JOIN wms.locations l ON s.location_id = l.id
             WHERE 
                t.task_type IN (1, 2) AND t.id = @TaskHeaderId
             """;

        return (await connection.QueryAsync<TaskDispatchLineResponse>(sql, request)).AsList();
    }
}
