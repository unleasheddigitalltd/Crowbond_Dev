using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Dispatches;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetPickingTaskDispatchLineDetails;

internal sealed class GetPickingTaskDispatchLineDetailsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPickingTaskDispatchLineDetailsQuery, TaskDispatchLineResponse>
{
    public async Task<Result<TaskDispatchLineResponse>> Handle(GetPickingTaskDispatchLineDetailsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 dl.id AS {nameof(TaskDispatchLineResponse.Id)},
                 t.id AS {nameof(TaskDispatchLineResponse.TaskId)},
                 t.task_type AS {nameof(TaskDispatchLineResponse.TaskType)},
                 p.id AS {nameof(TaskDispatchLineResponse.ProductId)},
                 p.name AS {nameof(TaskDispatchLineResponse.ProductName)},
                 p.unit_of_measure AS {nameof(TaskDispatchLineResponse.UnitOfMeasure)},
                 dl.ordered_qty AS {nameof(TaskDispatchLineResponse.OrderedQty)},
                 dl.picked_qty AS {nameof(TaskDispatchLineResponse.PickedQty)},
                 dl.customer_business_name AS {nameof(TaskDispatchLineResponse.CustomerBusinessName)},
                 dl.order_id AS {nameof(TaskDispatchLineResponse.OrderId)},
                 dl.order_line_id AS {nameof(TaskDispatchLineResponse.OrderLineId)},
                 dl.order_no AS {nameof(TaskDispatchLineResponse.OrderNo)},
                 dl.is_picked AS {nameof(TaskDispatchLineResponse.IsPicked)}
             FROM wms.task_headers t
             INNER JOIN wms.dispatch_headers d ON t.dispatch_id = d.id
             INNER JOIN wms.dispatch_lines dl ON dl.dispatch_header_id = d.id
             INNER JOIN wms.products p ON dl.product_id = p.id
             WHERE  
                    t.id = @TaskHeaderId AND
                    dl.id = @DispatchLineId
                             (
                                 (t.task_type = 2 AND p.unit_of_measure = 'kg') OR
                                 (t.task_type = 1)
                             )
             """;

        TaskDispatchLineResponse? product = await connection.QuerySingleOrDefaultAsync<TaskDispatchLineResponse>(sql, request);

        if (product is null)
        {
            return Result.Failure<TaskDispatchLineResponse>(DispatchErrors.LineNotFound(request.DispatchLineId));
        }

        return product;
    }
}
