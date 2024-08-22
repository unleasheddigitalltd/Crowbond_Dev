using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Domain.CustomerContacts;
using Dapper;

namespace Crowbond.Modules.CRM.Application.CustomerContacts.GetCustomerContact;

internal sealed class GetCustomerContactQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetCustomerContactQuery, CustomerContactResponse>
{
    public async Task<Result<CustomerContactResponse>> Handle(GetCustomerContactQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(CustomerContactResponse.Id)},
                 customer_id AS {nameof(CustomerContactResponse.CustomerId)},
                 first_name AS {nameof(CustomerContactResponse.FirstName)},
                 last_name AS {nameof(CustomerContactResponse.LastName)},
                 phone_number AS {nameof(CustomerContactResponse.PhoneNumber)},
                 email AS {nameof(CustomerContactResponse.Email)},
                 is_primary AS {nameof(CustomerContactResponse.IsPrimary)},
                 is_active AS {nameof(CustomerContactResponse.IsActive)}
             FROM crm.customer_contacts t
             WHERE id = @CustomerContactId
             """;

        CustomerContactResponse? customerContact = await connection.QuerySingleOrDefaultAsync<CustomerContactResponse>(sql, request);

        if (customerContact is null)
        {
            return Result.Failure<CustomerContactResponse>(CustomerContactErrors.NotFound(request.CustomerContactId));
        }

        return customerContact;
    }
}
