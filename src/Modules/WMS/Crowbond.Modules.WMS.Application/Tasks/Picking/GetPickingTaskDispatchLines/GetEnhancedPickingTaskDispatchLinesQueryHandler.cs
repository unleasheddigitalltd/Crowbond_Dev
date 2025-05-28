using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetPickingTaskDispatchLines;

internal sealed class GetEnhancedPickingTaskDispatchLinesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetEnhancedPickingTaskDispatchLinesQuery, IReadOnlyCollection<EnhancedTaskDispatchLineResponse>>
{
    public async Task<Result<IReadOnlyCollection<EnhancedTaskDispatchLineResponse>>> Handle(GetEnhancedPickingTaskDispatchLinesQuery request, CancellationToken cancellationToken)
    {
        await using var connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                dl.id AS {nameof(EnhancedTaskDispatchLineResponse.DispatchLineId)},
                p.id AS {nameof(EnhancedTaskDispatchLineResponse.ProductId)},
                p.name AS {nameof(EnhancedTaskDispatchLineResponse.ProductName)},
                p.unit_of_measure_name AS {nameof(EnhancedTaskDispatchLineResponse.UnitOfMeasure)},
                dl.ordered_qty AS {nameof(EnhancedTaskDispatchLineResponse.OrderedQty)},
                dl.picked_qty AS {nameof(EnhancedTaskDispatchLineResponse.PickedQty)},
                dl.is_picked AS {nameof(EnhancedTaskDispatchLineResponse.IsPicked)},
                COALESCE(l.name, 'No Location') AS {nameof(EnhancedTaskDispatchLineResponse.Location)},
                tl.id AS {nameof(EnhancedTaskDispatchLineResponse.TaskLineId)},
                tl.total_qty AS {nameof(EnhancedTaskDispatchLineResponse.TaskLineTotalQty)},
                tl.completed_qty AS {nameof(EnhancedTaskDispatchLineResponse.TaskLineCompletedQty)},
                (tl.completed_qty >= tl.total_qty) AS {nameof(EnhancedTaskDispatchLineResponse.TaskLineIsCompleted)}
             FROM wms.task_headers t
             INNER JOIN wms.dispatch_headers d ON t.dispatch_id = d.id
             INNER JOIN wms.dispatch_lines dl ON dl.dispatch_header_id = d.id
             INNER JOIN wms.products p ON dl.product_id = p.id
             LEFT JOIN wms.stocks s ON s.product_id = dl.product_id
             LEFT JOIN wms.locations l ON s.location_id = l.id
             LEFT JOIN wms.task_line_dispatch_mappings tldm ON tldm.dispatch_line_id = dl.id
             LEFT JOIN wms.task_lines tl ON tldm.task_line_id = tl.id AND tl.task_header_id = t.id
             WHERE 
                t.task_type IN (1, 2) AND t.id = @TaskHeaderId
             """;

        return (await connection.QueryAsync<EnhancedTaskDispatchLineResponse>(sql, request)).AsList();
    }
}