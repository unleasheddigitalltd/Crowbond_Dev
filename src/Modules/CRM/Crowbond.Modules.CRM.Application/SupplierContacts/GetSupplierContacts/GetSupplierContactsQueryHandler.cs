using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.CRM.Application.SupplierContacts.GetSupplierContacts;

internal sealed class GetSupplierContactsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetSupplierContactsQuery, IReadOnlyCollection<SupplierContactResponse>>
{
    public async Task<Result<IReadOnlyCollection<SupplierContactResponse>>> Handle(GetSupplierContactsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 t.id AS {nameof(SupplierContactResponse.Id)},
                 t.supplier_id AS {nameof(SupplierContactResponse.SupplierId)},
                 t.first_name AS {nameof(SupplierContactResponse.FirstName)},
                 t.last_name AS {nameof(SupplierContactResponse.LastName)},
                 t.phone_number AS {nameof(SupplierContactResponse.PhoneNumber)},
                 t.primary AS {nameof(SupplierContactResponse.Primary)},
                 t.is_active AS {nameof(SupplierContactResponse.IsActive)}
             FROM crm.supplier_contacts t
             INNER JOIN crm.suppliers c ON c.id = t.supplier_id
             WHERE c.id = @SupplierId;
             """;

        List<SupplierContactResponse> supplierContacts = (await connection.QueryAsync<SupplierContactResponse>(sql, request)).AsList();

        return supplierContacts;
    }
}
