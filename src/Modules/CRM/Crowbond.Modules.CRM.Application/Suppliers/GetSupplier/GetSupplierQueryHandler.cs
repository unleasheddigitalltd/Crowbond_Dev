using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
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
                 account_number AS {nameof(SupplierResponse.AccountNumber)},
                 supplier_name AS {nameof(SupplierResponse.SupplierName)},
                 address_line1 AS {nameof(SupplierResponse.AddressLine1)},
                 address_line2 AS {nameof(SupplierResponse.AddressLine2)},
                 town_city AS {nameof(SupplierResponse.TownCity)},
                 county AS {nameof(SupplierResponse.County)},
                 country AS {nameof(SupplierResponse.Country)},
                 postal_code AS {nameof(SupplierResponse.PostalCode)}                 
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
