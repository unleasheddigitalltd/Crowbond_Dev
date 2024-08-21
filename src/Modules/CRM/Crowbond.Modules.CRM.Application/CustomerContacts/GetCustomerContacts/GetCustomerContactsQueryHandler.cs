using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.CRM.Application.CustomerContacts.GetCustomerContacts;

internal sealed class GetCustomerContactsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetCustomerContactsQuery, IReadOnlyCollection<CustomerContactResponse>>
{
    public async Task<Result<IReadOnlyCollection<CustomerContactResponse>>> Handle(GetCustomerContactsQuery request, CancellationToken cancellationToken)
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
                 "primary" AS {nameof(CustomerContactResponse.Primary)},
                 is_active AS {nameof(CustomerContactResponse.IsActive)}
             FROM crm.customer_contacts
             WHERE customer_id = @CustomerId
             ORDER BY "primary" DESC, is_active DESC;
             """;

        List<CustomerContactResponse> customerContacts = (await connection.QueryAsync<CustomerContactResponse>(sql, request)).AsList();

        return customerContacts;
    }
}
