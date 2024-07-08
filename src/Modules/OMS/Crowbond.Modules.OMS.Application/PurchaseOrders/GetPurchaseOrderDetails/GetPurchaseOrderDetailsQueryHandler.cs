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
                 suppliername AS {nameof(PurchaseOrderDetailsResponse.SupplierName)},
                 accountnumber AS {nameof(PurchaseOrderDetailsResponse.PurchaseOrderNo)},
                 addressline1 AS {nameof(PurchaseOrderDetailsResponse.AddressLine1)},
                 addressline2 AS {nameof(PurchaseOrderDetailsResponse.AddressLine2)},
                 towncity AS {nameof(PurchaseOrderDetailsResponse.TownCity)},
                 postalcode AS {nameof(PurchaseOrderDetailsResponse.PostalCode)},
                 supplieremail AS {nameof(PurchaseOrderDetailsResponse.SupplierEmail)},
                 supplierphone AS {nameof(PurchaseOrderDetailsResponse.SupplierPhone)},
                 suppliercontact AS {nameof(PurchaseOrderDetailsResponse.SupplierContact)},
                 paymentterms AS {nameof(PurchaseOrderDetailsResponse.PurchaseOrderTotal)},
                 suppliernotes AS {nameof(PurchaseOrderDetailsResponse.SupplierNotes)}
                 
             FROM oms.purchase_order_headers
             WHERE id = @PurchaseOrderId
             """;

        PurchaseOrderDetailsResponse? purchaseorder = await connection.QuerySingleOrDefaultAsync<PurchaseOrderDetailsResponse>(sql, request);

        if (purchaseorder is null)
        {
            return Result.Failure<PurchaseOrderDetailsResponse>(PurchaseOrderErrors.NotFound(request.PurchaseOrderId));
        }

        return purchaseorder;
    }
}
