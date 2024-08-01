using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Domain.CustomerContacts;
using Dapper;

namespace Crowbond.Modules.CRM.Application.SupplierContacts.GetSupplierContactDetails;

internal sealed class GetSupplierContactDetailsQueryHandle(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetSupplierContactDetailsQuery, SupplierContactDetailsResponse>
{
    public async Task<Result<SupplierContactDetailsResponse>> Handle(GetSupplierContactDetailsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(SupplierContactDetailsResponse.Id)},
                 supplier_id AS {nameof(SupplierContactDetailsResponse.SupplierId)},
                 first_name AS {nameof(SupplierContactDetailsResponse.FirstName)},
                 last_name AS {nameof(SupplierContactDetailsResponse.LastName)},
                 phone_number AS {nameof(SupplierContactDetailsResponse.PhoneNumber)},
                 mobile AS {nameof(SupplierContactDetailsResponse.Mobile)},
                 username AS {nameof(SupplierContactDetailsResponse.Username)},
                 email AS {nameof(SupplierContactDetailsResponse.Email)},
                 "primary" AS {nameof(SupplierContactDetailsResponse.Primary)},
                 is_active AS {nameof(SupplierContactDetailsResponse.IsActive)}
             FROM crm.supplier_contacts
             WHERE id = @SupplierContactId
             """;

        SupplierContactDetailsResponse? supplierContact = await connection.QuerySingleOrDefaultAsync<SupplierContactDetailsResponse>(sql, request);

        if (supplierContact is null)
        {
            return Result.Failure<SupplierContactDetailsResponse>(CustomerContactErrors.NotFound(request.SupplierContactId));
        }

        return supplierContact;
    }
}
