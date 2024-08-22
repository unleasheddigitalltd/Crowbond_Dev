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
             WITH SupplierContacts AS (
                 SELECT
                     s.id AS {nameof(SupplierResponse.Id)},
                     s.account_number AS {nameof(SupplierResponse.AccountNumber)},
                     s.supplier_name AS {nameof(SupplierResponse.SupplierName)},
                     s.address_line1 AS {nameof(SupplierResponse.AddressLine1)},                    
                     s.address_line2 AS {nameof(SupplierResponse.AddressLine2)},
                     s.town_city AS {nameof(SupplierResponse.TownCity)},
                     s.county AS {nameof(SupplierResponse.County)},
                     s.country AS {nameof(SupplierResponse.Country)},
                     s.postal_code AS {nameof(SupplierResponse.PostalCode)},
                     s.is_active AS {nameof(SupplierResponse.IsActive)},
                     t.first_name AS {nameof(SupplierResponse.FirstName)},
                     t.last_name AS {nameof(SupplierResponse.LastName)},
                     t.phone_number AS {nameof(SupplierResponse.PhoneNumber)},
                     t.email AS {nameof(SupplierResponse.Email)},
                     ROW_NUMBER() OVER (PARTITION BY s.id ORDER BY t.primary DESC) AS FilterRowNum
                 FROM crm.suppliers s
                 LEFT JOIN crm.supplier_contacts t ON s.id = t.supplier_id
                 WHERE s.id = @SupplierId
             )
             SELECT *
             FROM SupplierContacts
             WHERE FilterRowNum = 1;
             """;

        SupplierResponse? supplier = await connection.QuerySingleOrDefaultAsync<SupplierResponse>(sql, request);

        if (supplier is null)
        {
            return Result.Failure<SupplierResponse>(SupplierErrors.NotFound(request.SupplierId));
        }

        return supplier;
    }
}
