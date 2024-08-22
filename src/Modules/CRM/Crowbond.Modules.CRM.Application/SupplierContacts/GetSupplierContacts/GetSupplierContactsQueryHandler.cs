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
                 id AS {nameof(SupplierContactResponse.Id)},
                 supplier_id AS {nameof(SupplierContactResponse.SupplierId)},
                 first_name AS {nameof(SupplierContactResponse.FirstName)},
                 last_name AS {nameof(SupplierContactResponse.LastName)},
                 phone_number AS {nameof(SupplierContactResponse.PhoneNumber)},
                 is_primary AS {nameof(SupplierContactResponse.IsPrimary)},
                 is_active AS {nameof(SupplierContactResponse.IsActive)}
             FROM crm.supplier_contacts
             WHERE supplier_id = @SupplierId             
             ORDER BY is_primary DESC, is_active DESC;
             """;

        List<SupplierContactResponse> supplierContacts = (await connection.QueryAsync<SupplierContactResponse>(sql, request)).AsList();

        return supplierContacts;
    }
}
