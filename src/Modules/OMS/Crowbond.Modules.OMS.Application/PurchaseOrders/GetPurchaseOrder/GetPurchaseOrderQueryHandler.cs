using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Domain.PurchaseOrderHeaders;
using Dapper;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrder;

internal sealed class GetPurchaseOrderQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPurchaseOrderQuery, PurchaseOrderResponse>
{
    public async Task<Result<PurchaseOrderResponse>> Handle(GetPurchaseOrderQuery request, CancellationToken cancellationToken)
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
                 shipping_location_name AS {nameof(PurchaseOrderResponse.ShippingLocationName)},
                 shipping_address_line1 AS {nameof(PurchaseOrderResponse.ShippingAddressLine1)},
                 shipping_address_line2 AS {nameof(PurchaseOrderResponse.ShippingAddressLine2)},
                 shipping_town_city AS {nameof(PurchaseOrderResponse.ShippingTownCity)},
                 shipping_county AS {nameof(PurchaseOrderResponse.ShippingCounty)},
                 shipping_country AS {nameof(PurchaseOrderResponse.ShippingCountry)},
                 shipping_postal_code AS {nameof(PurchaseOrderResponse.ShippingPostalCode)},
                 required_date AS {nameof(PurchaseOrderResponse.RequiredDate)},
                 purchase_order_amount AS {nameof(PurchaseOrderResponse.PurchaseOrderAmount)},
                 payment_status AS {nameof(PurchaseOrderResponse.PaymentStatus)},
                 purchase_order_notes AS {nameof(PurchaseOrderResponse.PurchaseOrderNotes)},
                 status AS {nameof(PurchaseOrderResponse.Status)}
             FROM oms.purchase_order_headers
             WHERE id = @PurchaseOrderHeaderId
             """;

        PurchaseOrderResponse? purchaseOrder = await connection.QuerySingleOrDefaultAsync<PurchaseOrderResponse>(sql, request);

        if (purchaseOrder is null)
        {
            return Result.Failure<PurchaseOrderResponse>(PurchaseOrderHeaderErrors.NotFound(request.PurchaseOrderHeaderId));
        }

        return purchaseOrder;
    }
}
