using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrderLines;

internal sealed class GetPurchaseOrderLinesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPurchaseOrderLinesQuery, IReadOnlyCollection<PurchaseOrderLineResponse>>
{
    public async Task<Result<IReadOnlyCollection<PurchaseOrderLineResponse>>> Handle(GetPurchaseOrderLinesQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                pl.id AS {nameof(PurchaseOrderLineResponse.Id)},
                pl.purchase_order_header_id AS {nameof(PurchaseOrderLineResponse.PurchaseOrderHeaderId)}, 
                pl.product_id AS {nameof(PurchaseOrderLineResponse.ProductId)}, 
                pl.product_sku AS {nameof(PurchaseOrderLineResponse.ProductSku)},
                pl.product_name AS {nameof(PurchaseOrderLineResponse.ProductName)},
                pl.unit_of_measure_name AS {nameof(PurchaseOrderLineResponse.UnitOfMeasureName)},
                pl.unit_price AS {nameof(PurchaseOrderLineResponse.UnitPrice)},
                pl.qty AS {nameof(PurchaseOrderLineResponse.Qty)},
                pl.sub_total AS {nameof(PurchaseOrderLineResponse.SubTotal)},
                pl.tax AS {nameof(PurchaseOrderLineResponse.Tax)},
                pl.line_total AS {nameof(PurchaseOrderLineResponse.LineTotal)},
                pl.foc AS {nameof(PurchaseOrderLineResponse.FOC)},
                pl.taxable AS {nameof(PurchaseOrderLineResponse.Taxable)},
                pl.comments AS {nameof(PurchaseOrderLineResponse.Comments)}
             FROM 
                oms.purchase_order_lines pl
                INNER JOIN oms.purchase_order_headers p ON p.id = pl.purchase_order_header_id
             WHERE
                p.id = @PurchaseOrderHeaderId
             """;

        List<PurchaseOrderLineResponse> purchaseOrderLines = (await connection.QueryAsync<PurchaseOrderLineResponse>(sql, request)).AsList();

        return purchaseOrderLines;
    }
}
