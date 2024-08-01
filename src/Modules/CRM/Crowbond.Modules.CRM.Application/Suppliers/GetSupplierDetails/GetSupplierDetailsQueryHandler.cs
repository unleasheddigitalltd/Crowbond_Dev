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
                 payment_terms AS {nameof(SupplierDetailsResponse.PaymentTerms)},
                 supplier_notes AS {nameof(SupplierDetailsResponse.SupplierNotes)}                 
             FROM crm.suppliers
             WHERE id = @SupplierId;

             SELECT
                 t.id AS {nameof(SupplierContactResponse.Id)},
                 t.supplier_id AS {nameof(SupplierContactResponse.SupplierId)},
                 t.first_name AS {nameof(SupplierContactResponse.FirstName)},
                 t.last_name AS {nameof(SupplierContactResponse.LastName)},
                 t.phone_number AS {nameof(SupplierContactResponse.PhoneNumber)},
                 t.primary AS {nameof(SupplierContactResponse.Primary)},
                 t.is_active AS {nameof(SupplierContactResponse.IsActive)}
             FROM crm.supplier_contacts t
             INNER JOIN crm.suppliers s ON s.id = t.supplier_id
             WHERE s.id = @SupplierId;
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
