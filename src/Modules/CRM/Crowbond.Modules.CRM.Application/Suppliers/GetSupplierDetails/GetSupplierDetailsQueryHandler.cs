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
                 account_number AS {nameof(SupplierDetailsResponse.AccountNumber)},
                 supplier_name AS {nameof(SupplierDetailsResponse.SupplierName)},
                 address_line1 AS {nameof(SupplierDetailsResponse.AddressLine1)},
                 address_line2 AS {nameof(SupplierDetailsResponse.AddressLine2)},
                 town_city AS {nameof(SupplierDetailsResponse.TownCity)},
                 county AS {nameof(SupplierDetailsResponse.County)},
                 country AS {nameof(SupplierDetailsResponse.Country)},
                 postal_code AS {nameof(SupplierDetailsResponse.PostalCode)},
                 supplier_notes AS {nameof(SupplierDetailsResponse.SupplierNotes)},
                 is_active AS {nameof(SupplierDetailsResponse.IsActive)}
             FROM crm.suppliers
             WHERE id = @SupplierId;

             SELECT
                 id AS {nameof(SupplierContactResponse.Id)},
                 supplier_id AS {nameof(SupplierContactResponse.SupplierId)},
                 first_name AS {nameof(SupplierContactResponse.FirstName)},
                 last_name AS {nameof(SupplierContactResponse.LastName)},
                 phone_number AS {nameof(SupplierContactResponse.PhoneNumber)},
                 is_primary AS {nameof(SupplierContactResponse.Primary)},
                 is_active AS {nameof(SupplierContactResponse.IsActive)}
             FROM crm.supplier_contacts
             WHERE supplier_id = @SupplierId
             ORDER BY is_primary DESC, is_active DESC;
             """;

        SqlMapper.GridReader multi = await connection.QueryMultipleAsync(sql, request);

        var suppliers = (await multi.ReadAsync<SupplierDetailsResponse>()).ToList();
        var supplierContacts = (await multi.ReadAsync<SupplierContactResponse>()).ToList();

        SupplierDetailsResponse? supplier = suppliers.SingleOrDefault();

        if (supplier is null)
        {
            return Result.Failure<SupplierDetailsResponse>(SupplierErrors.NotFound(request.SupplierId));
        }

        supplier.SupplierContacts = supplierContacts.Where(a => a.SupplierId == supplier.Id).ToList();

        return supplier;
    }
}
