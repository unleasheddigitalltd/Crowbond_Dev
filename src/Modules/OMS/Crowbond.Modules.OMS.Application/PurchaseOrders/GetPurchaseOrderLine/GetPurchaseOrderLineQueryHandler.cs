using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Domain.PurchaseOrders;
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
                category_id AS {nameof(PurchaseOrderLineResponse.CategoryId)},
                category_name AS {nameof(PurchaseOrderLineResponse.CategoryName)},
                brand_id AS {nameof(PurchaseOrderLineResponse.BrandId)},
                brand_name AS {nameof(PurchaseOrderLineResponse.BrandName)},
                product_group_id AS {nameof(PurchaseOrderLineResponse.ProductGroupId)},
                product_group_name AS {nameof(PurchaseOrderLineResponse.ProductGroupName)},
                unit_price AS {nameof(PurchaseOrderLineResponse.UnitPrice)},
                qty AS {nameof(PurchaseOrderLineResponse.Qty)},
                sub_total AS {nameof(PurchaseOrderLineResponse.SubTotal)},
                tax_rate_type AS {nameof(PurchaseOrderLineResponse.TaxRateType)},
                tax AS {nameof(PurchaseOrderLineResponse.Tax)},
                line_total AS {nameof(PurchaseOrderLineResponse.LineTotal)},
                comments AS {nameof(PurchaseOrderLineResponse.Comments)}
             FROM 
                oms.purchase_order_lines
             WHERE
                id = @PurchaseOrderLineId
             """;

        PurchaseOrderLineResponse? purchaseOrderLine = await connection.QuerySingleOrDefaultAsync<PurchaseOrderLineResponse>(sql, request);

        if (purchaseOrderLine == null)
        {
            return Result.Failure<PurchaseOrderLineResponse>(PurchaseOrderErrors.LineNotFound(request.PurchaseOrderLineId));
        }

        return purchaseOrderLine;
    }
}
