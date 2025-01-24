using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Receipts;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Receipts.GetReceiptLine;

internal sealed class GetReceiptLineQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetReceiptLineQuery, ReceiptLineResponse>
{
    public async Task<Result<ReceiptLineResponse>> Handle(GetReceiptLineQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                r.id AS {nameof(ReceiptLineResponse.Id)},
                p.sku AS {nameof(ReceiptLineResponse.ProductSku)},
                p.name AS {nameof(ReceiptLineResponse.ProductName)},
                p.unit_of_measure_name AS {nameof(ReceiptLineResponse.UnitOfMeasure)},
                r.received_qty AS {nameof(ReceiptLineResponse.ReceivedQty)},
                r.stored_qty AS {nameof(ReceiptLineResponse.StoredQty)},
                r.unit_price AS {nameof(ReceiptLineResponse.UnitPrice)},
                r.sell_by_date AS {nameof(ReceiptLineResponse.SellByDate)},
                r.use_by_date AS {nameof(ReceiptLineResponse.UseByDate)},
                r.batch_number AS {nameof(ReceiptLineResponse.Batch)},
                r.is_stored As {nameof(ReceiptLineResponse.IsStored)}
             FROM wms.receipt_lines r
             INNER JOIN wms.products p ON p.id = r.product_id
             WHERE
                r.id = @ReceiptLineId
             """;

        ReceiptLineResponse? receiptLine = await connection.QuerySingleOrDefaultAsync<ReceiptLineResponse>(sql, request);

        if (receiptLine == null)
        {
            return Result.Failure<ReceiptLineResponse>(ReceiptErrors.LineNotFound(request.ReceiptLineId));
        }

        return receiptLine;
    }
}
