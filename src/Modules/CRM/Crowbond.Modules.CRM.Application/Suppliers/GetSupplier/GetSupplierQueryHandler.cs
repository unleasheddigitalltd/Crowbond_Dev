using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Suppliers.GetSupplierDetails;
using Crowbond.Modules.CRM.Domain.Suppliers;
using Dapper;

namespace Crowbond.Modules.CRM.Application.Suppliers.GetSupplier;

internal sealed class GetSupplierQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetSupplierQuery, SupplierResponse>
{
    public async Task<Result<SupplierResponse>> Handle(GetSupplierQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(SupplierResponse.Id)},
                 businessname AS {nameof(SupplierResponse.SupplierName)},
                 accountnumber AS {nameof(SupplierResponse.AccountNumber)},
                 shippingaddressline1 AS {nameof(SupplierResponse.AddressLine1)},
                 shippingaddressline2 AS {nameof(SupplierResponse.AddressLine2)},
                 shippingtowncity AS {nameof(SupplierResponse.AddressTownCity)},
                 shippingpostalcode AS {nameof(SupplierResponse.AddressPostalCode)},
                 supplieremail AS {nameof(SupplierResponse.SupplierEmail)},
                 supplierphone AS {nameof(SupplierResponse.SupplierPhone)},
                 suppliercontact AS {nameof(SupplierResponse.SupplierContact)},
                 billingaddressline1 AS {nameof(SupplierResponse.BillingAddressLine1)},
                 billingaddressline2 AS {nameof(SupplierResponse.BillingAddressLine2)},
                 billingaddresstowncity AS {nameof(SupplierResponse.BillingAddressTownCity)},
                 billingaddresspostalcode AS {nameof(SupplierResponse.BillingAddressPostalCode)},
                 paymentterms AS {nameof(SupplierResponse.PaymentTerms)},
                 suppliernotes AS {nameof(SupplierResponse.SupplierNotes)}
                 
             FROM crm.suppliers
             WHERE id = @SupplierId
             """;

        SupplierResponse? supplier = await connection.QuerySingleOrDefaultAsync<SupplierResponse>(sql, request);

        if (supplier is null)
        {
            return Result.Failure<SupplierResponse>(SupplierErrors.NotFound(request.SupplierId));
        }

        return supplier;
    }
}
