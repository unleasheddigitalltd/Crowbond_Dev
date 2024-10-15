using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Domain.PurchaseOrders;
using Dapper;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrderDetails;

internal sealed class GetSupplierDetailsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPurchaseOrderDetailsQuery, PurchaseOrderDetailsResponse>
{
    public async Task<Result<PurchaseOrderDetailsResponse>> Handle(GetPurchaseOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                id AS {nameof(PurchaseOrderDetailsResponse.Id)}, 
                purchase_order_no AS {nameof(PurchaseOrderDetailsResponse.PurchaseOrderNo)},
                purchase_date AS {nameof(PurchaseOrderDetailsResponse.PurchaseDate)},
                paid_by AS {nameof(PurchaseOrderDetailsResponse.PaidBy)},
                paid_date AS {nameof(PurchaseOrderDetailsResponse.PaidDate)},
                supplier_id AS {nameof(PurchaseOrderDetailsResponse.SupplierId)},
                supplier_name AS {nameof(PurchaseOrderDetailsResponse.SupplierName)},
                contact_full_name AS {nameof(PurchaseOrderDetailsResponse.ContactFullName)},
                contact_phone AS {nameof(PurchaseOrderDetailsResponse.ContactPhone)},
                contact_email AS {nameof(PurchaseOrderDetailsResponse.ContactEmail)},
                purchase_order_amount AS {nameof(PurchaseOrderDetailsResponse.PurchaseOrderAmount)},
                shipping_location_name AS {nameof(PurchaseOrderDetailsResponse.ShippingLocationName)},
                shipping_address_line1 AS {nameof(PurchaseOrderDetailsResponse.ShippingAddressLine1)},
                shipping_address_line2 AS {nameof(PurchaseOrderDetailsResponse.ShippingAddressLine2)},
                shipping_town_city AS {nameof(PurchaseOrderDetailsResponse.ShippingTownCity)},
                shipping_county AS {nameof(PurchaseOrderDetailsResponse.ShippingCounty)},
                shipping_country AS {nameof(PurchaseOrderDetailsResponse.ShippingCountry)},
                shipping_postal_code AS {nameof(PurchaseOrderDetailsResponse.ShippingPostalCode)},
                required_date AS {nameof(PurchaseOrderDetailsResponse.RequiredDate)},
                expected_shipping_date AS {nameof(PurchaseOrderDetailsResponse.ExpectedShippingDate)},
                supplier_reference AS {nameof(PurchaseOrderDetailsResponse.SupplierReference)},
                purchase_order_tax AS {nameof(PurchaseOrderDetailsResponse.PurchaseOrderTax)},
                delivery_method AS {nameof(PurchaseOrderDetailsResponse.DeliveryMethod)},
                delivery_charge AS {nameof(PurchaseOrderDetailsResponse.DeliveryCharge)},
                payment_method AS {nameof(PurchaseOrderDetailsResponse.PaymentMethod)},
                payment_status AS {nameof(PurchaseOrderDetailsResponse.PaymentStatus)},
                purchase_order_notes AS {nameof(PurchaseOrderDetailsResponse.PurchaseOrderNotes)},
                sales_order_ref AS {nameof(PurchaseOrderDetailsResponse.SalesOrderRef)},
                tags AS { nameof(PurchaseOrderDetailsResponse.Tags)},
                status AS {nameof(PurchaseOrderDetailsResponse.Status)}
             FROM oms.purchase_order_headers
             WHERE id = @PurchaseOrderHeaderId
             """;

        PurchaseOrderDetailsResponse? purchaseorder = await connection.QuerySingleOrDefaultAsync<PurchaseOrderDetailsResponse>(sql, request);

        if (purchaseorder is null)
        {
            return Result.Failure<PurchaseOrderDetailsResponse>(PurchaseOrderErrors.NotFound(request.PurchaseOrderHeaderId));
        }

        return purchaseorder;
    }
}
