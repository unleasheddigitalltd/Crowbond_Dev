using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Receipts;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.GetPutAwayTaskReceiptLineDetails;

internal sealed class GetPutAwayTaskReceiptLineDetailsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPutAwayTaskReceiptLineDetailsQuery, TaskReceiptLineResponse>
{
    public async Task<Result<TaskReceiptLineResponse>> Handle(GetPutAwayTaskReceiptLineDetailsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 rl.id AS {nameof(TaskReceiptLineResponse.Id)},
                 t.id AS {nameof(TaskReceiptLineResponse.TaskId)},
                 p.name AS {nameof(TaskReceiptLineResponse.ProductName)},
                 rl.received_qty AS {nameof(TaskReceiptLineResponse.ReceivedQty)},
                 rl.stored_qty AS {nameof(TaskReceiptLineResponse.StoredQty)},
                 rl.missed_qty AS {nameof(TaskReceiptLineResponse.MissedQty)},
                 rl.batch_number AS {nameof(TaskReceiptLineResponse.BatchNumber)},
                 rl.sell_by_date AS {nameof(TaskReceiptLineResponse.SellByDate)},
                 rl.use_by_date AS {nameof(TaskReceiptLineResponse.UseByDate)},
                 r.location_id AS {nameof(TaskReceiptLineResponse.LocationId)},
                 rl.is_stored AS {nameof(TaskReceiptLineResponse.IsStored)}
             FROM wms.task_headers t
             INNER JOIN wms.receipt_headers r ON t.receipt_id = r.id
             INNER JOIN wms.receipt_lines rl ON rl.receipt_header_id = r.id
             INNER JOIN wms.products p ON rl.product_id = p.id
             WHERE  
                    t.id = @TaskHeaderId AND
                    rl.id = @ReceiptLineId
             """;

        TaskReceiptLineResponse? product = await connection.QuerySingleOrDefaultAsync<TaskReceiptLineResponse>(sql, request);

        if (product is null)
        {
            return Result.Failure<TaskReceiptLineResponse>(ReceiptErrors.LineNotFound(request.ReceiptLineId));
        }

        return product;   
    }
}
