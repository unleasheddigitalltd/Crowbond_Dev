using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Receipts.GetReceiptLines.Dtos;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Receipts.GetReceiptLines;

internal sealed class GetReceiptLinesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetReceiptLinesQuery, IReadOnlyCollection<ReceiptLineResponse>>
{
    public async Task<Result<IReadOnlyCollection<ReceiptLineResponse>>> Handle(GetReceiptLinesQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                r.id AS {nameof(ReceiptLineResponse.Id)},
                p.sku AS {nameof(ReceiptLineResponse.ProductSku)},
                p.name AS {nameof(ReceiptLineResponse.ProductName)},
                p.unit_of_measure_name AS {nameof(ReceiptLineResponse.UnitOfMeasure)},
                r.quantity_received AS {nameof(ReceiptLineResponse.QuantityReceived)},
                r.unit_price AS {nameof(ReceiptLineResponse.UnitPrice)},
                r.sell_by_date AS {nameof(ReceiptLineResponse.SellByDate)},
                r.use_by_date AS {nameof(ReceiptLineResponse.UseByDate)},
                r.batch_number AS {nameof(ReceiptLineResponse.Batch)}
             FROM wms.receipt_lines r
             INNER JOIN wms.products p ON p.id = r.product_id
             WHERE
                r.receipt_header_id = @ReceiptHeaderId
             """;

        List<ReceiptLineResponse> receiptLines = (await connection.QueryAsync<ReceiptLineResponse>(sql, request)).AsList();

        return receiptLines;
    }
}
