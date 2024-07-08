using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrderDetails;
using Crowbond.Modules.OMS.Domain.PurchaseOrders;
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
                 businessname AS {nameof(PurchaseOrderResponse.SupplierName)},
                 accountnumber AS {nameof(PurchaseOrderResponse.PurchaseOrderNumber)},
                 shippingaddressline1 AS {nameof(PurchaseOrderResponse.AddressLine1)},
                 shippingaddressline2 AS {nameof(PurchaseOrderResponse.AddressLine2)},
                 shippingtowncity AS {nameof(PurchaseOrderResponse.AddressTownCity)},
                 shippingpostalcode AS {nameof(PurchaseOrderResponse.AddressPostalCode)},
                 supplieremail AS {nameof(PurchaseOrderResponse.SupplierEmail)},
                 supplierphone AS {nameof(PurchaseOrderResponse.SupplierPhone)},
                 suppliercontact AS {nameof(PurchaseOrderResponse.SupplierContact)},
                 billingaddressline1 AS {nameof(PurchaseOrderResponse.BillingAddressLine1)},
                 billingaddressline2 AS {nameof(PurchaseOrderResponse.BillingAddressLine2)},
                 billingaddresstowncity AS {nameof(PurchaseOrderResponse.BillingAddressTownCity)},
                 billingaddresspostalcode AS {nameof(PurchaseOrderResponse.BillingAddressPostalCode)},
                 paymentterms AS {nameof(PurchaseOrderResponse.PaymentTerms)},
                 suppliernotes AS {nameof(PurchaseOrderResponse.SupplierNotes)}
                 
             FROM oms.purchaseorders
             WHERE id = @PurchaseOrderId
             """;

        PurchaseOrderResponse? purchaseOrder = await connection.QuerySingleOrDefaultAsync<PurchaseOrderResponse>(sql, request);

        if (purchaseOrder is null)
        {
            return Result.Failure<PurchaseOrderResponse>(PurchaseOrderErrors.NotFound(request.PurchaseOrderId));
        }

        return purchaseOrder;
    }
}
