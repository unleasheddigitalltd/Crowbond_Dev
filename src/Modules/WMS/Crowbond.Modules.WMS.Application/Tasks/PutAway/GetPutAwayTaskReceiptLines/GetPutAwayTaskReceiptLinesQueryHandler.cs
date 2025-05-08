using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.GetPutAwayTaskReceiptLines;

internal sealed class GetPutAwayTaskReceiptLinesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPutAwayTaskReceiptLinesQuery, IReadOnlyCollection<TaskReceiptLineResponse>>
{
    public async Task<Result<IReadOnlyCollection<TaskReceiptLineResponse>>> Handle(GetPutAwayTaskReceiptLinesQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                rl.id AS {nameof(TaskReceiptLineResponse.ReceiptLineId)},
                p.name AS {nameof(TaskReceiptLineResponse.ProductName)},
                rl.received_qty AS {nameof(TaskReceiptLineResponse.ReceivedQty)},
                rl.stored_qty AS {nameof(TaskReceiptLineResponse.StoredQty)},
                rl.missed_qty AS {nameof(TaskReceiptLineResponse.MissedQty)},
                rl.is_stored AS {nameof(TaskReceiptLineResponse.IsStored)}
             FROM  wms.task_headers t
             INNER JOIN wms.receipt_headers r ON t.receipt_id = r.id
             INNER JOIN wms.receipt_lines rl ON rl.receipt_header_id = r.id
             INNER JOIN wms.products p ON rl.product_id = p.id
             WHERE 
                t.id = @TaskHeaderId
             """;

        List<TaskReceiptLineResponse> receiptLines = (await connection.QueryAsync<TaskReceiptLineResponse>(sql, request)).AsList();

        return receiptLines;
    }
}
