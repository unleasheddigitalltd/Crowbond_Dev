using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetMyPickingTaskAssignmentLines;

internal sealed class GetMyPickingTaskAssignmentDispatchLinesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetMyPickingTaskAssignmentDispatchLinesQuery, IReadOnlyCollection<DispatchLineResponse>>
{
    public async Task<Result<IReadOnlyCollection<DispatchLineResponse>>> Handle(GetMyPickingTaskAssignmentDispatchLinesQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                dl.id AS {nameof(DispatchLineResponse.DispatchLineId)},
                p.sku AS {nameof(DispatchLineResponse.ProductSku)},
                p.name AS {nameof(DispatchLineResponse.ProductName)},
                dl.is_picked AS {nameof(DispatchLineResponse.IsPicked)}
             FROM wms.wms.task_headers t
             INNER JOIN wms.dispatch_headers d ON d.id = t.dispatch_id
             INNER JOIN wms.dispatch_lines dl ON d.id = dl.dispatch_header_id
             INNER JOIN wms.products p ON tl.product_id = p.id
             WHERE 
                t.id = @TaskHeaderId
             """;

        List<DispatchLineResponse> dispatchLines = (await connection.QueryAsync<DispatchLineResponse>(sql, request)).AsList();

        return dispatchLines;
    }
}
