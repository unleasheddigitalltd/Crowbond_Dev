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
                 p.id AS {nameof(PurchaseOrderResponse.Id)},          
                 p.purchase_order_no AS {nameof(PurchaseOrderResponse.PurchaseOrderNo)},
                 p.purchase_date AS {nameof(PurchaseOrderResponse.PurchaseDate)},
                 p.supplier_name AS {nameof(PurchaseOrderResponse.SupplierName)},
                 p.contact_full_name AS {nameof(PurchaseOrderResponse.ContactFullName)},
                 p.contact_phone AS {nameof(PurchaseOrderResponse.ContactPhone)},
                 p.contact_email AS {nameof(PurchaseOrderResponse.ContactEmail)},
                 p.required_date AS {nameof(PurchaseOrderResponse.RequiredDate)},
                 p.purchase_order_amount AS {nameof(PurchaseOrderResponse.PurchaseOrderAmount)},
                 p.purchase_order_notes AS {nameof(PurchaseOrderResponse.PurchaseOrderNotes)},
                 p.created_by AS {nameof(PurchaseOrderResponse.CreatedBy)},
                 p.created_on_utc AS {nameof(PurchaseOrderResponse.CreatedOnUtc)}
                 l.id AS {nameof(PurchaseOrderLineResponse.PurchaseOrderLineId)},
                 l.purchase_order_header_id AS {nameof(PurchaseOrderLineResponse.PurchaseOrderHeaderId)},
                 l.product_id AS {nameof(PurchaseOrderLineResponse.ProductId)},
                 l.qty AS {nameof(PurchaseOrderLineResponse.Qty)},
                 l.unit_price AS {nameof(PurchaseOrderLineResponse.UnitPrice)}
             FROM oms.purchase_order_headers p
             INNER JOIN oms.purchase_order_lines l ON p.id = l.purchase_order_header_id
             WHERE p.id = @PurchaseOrderHeaderId
             """;

        Dictionary<Guid, PurchaseOrderResponse> purchaseOrdersDictionary = [];
        await connection.QueryAsync<PurchaseOrderResponse, PurchaseOrderLineResponse, PurchaseOrderResponse>(
            sql,
            (purchaseOrder, purchaseOrderLine) =>
            {
                if (purchaseOrdersDictionary.TryGetValue(purchaseOrder.Id, out PurchaseOrderResponse? existingEvent))
                {
                    purchaseOrder = existingEvent;
                }
                else
                {
                    purchaseOrdersDictionary.Add(purchaseOrder.Id, purchaseOrder);
                }

                purchaseOrder.Lines.Add(purchaseOrderLine);

                return purchaseOrder;
            },
            request,
            splitOn: nameof(PurchaseOrderLineResponse.PurchaseOrderLineId));

        if (!purchaseOrdersDictionary.TryGetValue(request.PurchaseOrderHeaderId, out PurchaseOrderResponse purchaseOrder))
        {
            return Result.Failure<PurchaseOrderResponse>(PurchaseOrderErrors.NotFound(request.PurchaseOrderHeaderId));
        }

        return purchaseOrder;
    }
}
