using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrderLines;
using Crowbond.Modules.OMS.Domain.PurchaseOrderLines;
using Dapper;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrderLine;

internal sealed class GetPurchaseOrderLineQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPurchaseOrderLineQuery, PurchaseOrderLineResponse>
{
    public async Task<Result<PurchaseOrderLineResponse>> Handle(GetPurchaseOrderLineQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                id AS {nameof(PurchaseOrderLineResponse.Id)},
                purchase_order_header_id AS {nameof(PurchaseOrderLineResponse.PurchaseOrderHeaderId)}, 
                product_id AS {nameof(PurchaseOrderLineResponse.ProductId)}, 
                product_sku AS {nameof(PurchaseOrderLineResponse.ProductSku)},
                product_name AS {nameof(PurchaseOrderLineResponse.ProductName)},
                unit_of_measure_name AS {nameof(PurchaseOrderLineResponse.UnitOfMeasureName)},
                unit_price AS {nameof(PurchaseOrderLineResponse.UnitPrice)},
                qty AS {nameof(PurchaseOrderLineResponse.Qty)},
                sub_total AS {nameof(PurchaseOrderLineResponse.SubTotal)},
                tax AS {nameof(PurchaseOrderLineResponse.Tax)},
                line_total AS {nameof(PurchaseOrderLineResponse.LineTotal)},
                foc AS {nameof(PurchaseOrderLineResponse.FOC)},
                taxable AS {nameof(PurchaseOrderLineResponse.Taxable)},
                comments AS {nameof(PurchaseOrderLineResponse.Comments)}
             FROM 
                oms.purchase_order_lines
             WHERE
                id = @PurchaseOrderLineId
             """;

        PurchaseOrderLineResponse? purchaseOrderLine = await connection.QuerySingleOrDefaultAsync<PurchaseOrderLineResponse>(sql, request);

        if (purchaseOrderLine is null)
        {
            return Result.Failure<PurchaseOrderLineResponse>(PurchaseOrderLineErrors.NotFound(request.PurchaseOrderLineId));
        }

        return purchaseOrderLine;
    }
}
