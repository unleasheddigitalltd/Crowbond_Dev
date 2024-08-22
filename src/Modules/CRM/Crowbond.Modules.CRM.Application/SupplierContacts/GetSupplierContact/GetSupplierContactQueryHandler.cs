using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Domain.CustomerContacts;
using Dapper;

namespace Crowbond.Modules.CRM.Application.SupplierContacts.GetSupplierContact;

internal sealed class GetSupplierContactQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetSupplierContactQuery, SupplierContactResponse>
{
    public async Task<Result<SupplierContactResponse>> Handle(GetSupplierContactQuery request, CancellationToken cancellationToken)
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
             WHERE id = @SupplierContactId
             """;

        SupplierContactResponse? supplierContact = await connection.QuerySingleOrDefaultAsync<SupplierContactResponse>(sql, request);

        if (supplierContact is null)
        {
            return Result.Failure<SupplierContactResponse>(CustomerContactErrors.NotFound(request.SupplierContactId));
        }

        return supplierContact;
    }
}
