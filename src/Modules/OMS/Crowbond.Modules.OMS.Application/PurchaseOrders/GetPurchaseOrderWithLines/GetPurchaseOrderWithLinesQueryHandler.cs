using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Domain.PurchaseOrders;
using Dapper;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrderWithLines;

internal sealed class GetPurchaseOrderWithLinesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPurchaseOrderWithLinesQuery, PurchaseOrderResponse>
{
    public async Task<Result<PurchaseOrderResponse>> Handle(GetPurchaseOrderWithLinesQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(PurchaseOrderResponse.Id)},          
                 purchase_order_no AS {nameof(PurchaseOrderResponse.PurchaseOrderNo)},
                 purchase_date AS {nameof(PurchaseOrderResponse.PurchaseDate)},
                 supplier_name AS {nameof(PurchaseOrderResponse.SupplierName)},
                 contact_full_name AS {nameof(PurchaseOrderResponse.ContactFullName)},
                 contact_phone AS {nameof(PurchaseOrderResponse.ContactPhone)},
                 contact_email AS {nameof(PurchaseOrderResponse.ContactEmail)},
                 required_date AS {nameof(PurchaseOrderResponse.RequiredDate)},
                 purchase_order_amount AS {nameof(PurchaseOrderResponse.PurchaseOrderAmount)},
                 purchase_order_notes AS {nameof(PurchaseOrderResponse.PurchaseOrderNotes)},
                 created_by AS {nameof(PurchaseOrderResponse.CreatedBy)},
                 created_on_utc AS {nameof(PurchaseOrderResponse.CreatedOnUtc)}
             FROM oms.purchase_order_headers
             WHERE id = @PurchaseOrderHeaderId;

             SELECT
                 l.id AS {nameof(PurchaseOrderLineResponse.Id)},
                 l.purchase_order_header_id AS {nameof(PurchaseOrderLineResponse.PurchaseOrderHeaderId)},
                 l.product_id AS {nameof(PurchaseOrderLineResponse.ProductId)},
                 l.qty AS {nameof(PurchaseOrderLineResponse.Qty)},
                 l.unit_price AS {nameof(PurchaseOrderLineResponse.UnitPrice)}
             FROM oms.purchase_order_lines l
             INNER JOIN oms.purchase_order_headers p ON p.id = l.purchase_order_header_id
             WHERE p.id = @PurchaseOrderHeaderId
             """;


        SqlMapper.GridReader multi = await connection.QueryMultipleAsync(sql, request);

        var purchaseOrders = (await multi.ReadAsync<PurchaseOrderResponse>()).ToList();
        var purchaseOrderLines = (await multi.ReadAsync<PurchaseOrderLineResponse>()).ToList();

        PurchaseOrderResponse? purchaseOrder = purchaseOrders.SingleOrDefault();

        if (purchaseOrder is null)
        {
            return Result.Failure<PurchaseOrderResponse>(PurchaseOrderErrors.NotFound(request.PurchaseOrderHeaderId));
        }

        purchaseOrder.Lines = purchaseOrderLines.Where(a => a.PurchaseOrderHeaderId == purchaseOrder.Id).ToList();

        return purchaseOrder;
    }
}
