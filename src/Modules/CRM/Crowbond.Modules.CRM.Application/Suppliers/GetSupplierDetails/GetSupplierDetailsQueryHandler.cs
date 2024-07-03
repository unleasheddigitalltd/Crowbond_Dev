using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Domain.Suppliers;
using Dapper;

namespace Crowbond.Modules.CRM.Application.Suppliers.GetSupplierDetails;

internal sealed class GetSupplierDetailsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetSupplierDetailsQuery, SupplierDetailsResponse>
{
    public async Task<Result<SupplierDetailsResponse>> Handle(GetSupplierDetailsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(SupplierDetailsResponse.Id)},
                 suppliername AS {nameof(SupplierDetailsResponse.SupplierName)},
                 accountnumber AS {nameof(SupplierDetailsResponse.AccountNumber)},
                 addressline1 AS {nameof(SupplierDetailsResponse.AddressLine1)},
                 addressline2 AS {nameof(SupplierDetailsResponse.AddressLine2)},
                 towncity AS {nameof(SupplierDetailsResponse.TownCity)},
                 postalcode AS {nameof(SupplierDetailsResponse.PostalCode)},
                 supplieremail AS {nameof(SupplierDetailsResponse.SupplierEmail)},
                 supplierphone AS {nameof(SupplierDetailsResponse.SupplierPhone)},
                 suppliercontact AS {nameof(SupplierDetailsResponse.SupplierContact)},
                 billingaddressline1 AS {nameof(SupplierDetailsResponse.BillingAddressLine1)},
                 billingaddressline2 AS {nameof(SupplierDetailsResponse.BillingAddressLine2)},
                 billingtowncity AS {nameof(SupplierDetailsResponse.BillingTownCity)},
                 billingpostalcode AS {nameof(SupplierDetailsResponse.BillingPostalCode)},
                 paymentterms AS {nameof(SupplierDetailsResponse.PaymentTerms)},
                 suppliernotes AS {nameof(SupplierDetailsResponse.SupplierNotes)}
                 
             FROM crm.suppliers
             WHERE id = @SupplierId
             """;

        SupplierDetailsResponse? supplier = await connection.QuerySingleOrDefaultAsync<SupplierDetailsResponse>(sql, request);

        if (supplier is null)
        {
            return Result.Failure<SupplierDetailsResponse>(SupplierErrors.NotFound(request.SupplierId));
        }

        return supplier;
    }
}
