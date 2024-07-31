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
                 t.id AS {nameof(CustomerContactResponse.Id)},
                 t.customer_id AS {nameof(CustomerContactResponse.CustomerId)},
                 t.first_name AS {nameof(CustomerContactResponse.FirstName)},
                 t.last_name AS {nameof(CustomerContactResponse.LastName)},
                 t.phone_number AS {nameof(CustomerContactResponse.PhoneNumber)},
                 t.primary AS {nameof(CustomerContactResponse.Primary)},
                 t.is_active AS {nameof(CustomerContactResponse.IsActive)}
             FROM crm.customer_contacts t
             INNER JOIN crm.customers c ON c.id = t.customer_id
             WHERE c.id = @CustomerId;
             """;

        List<CustomerContactResponse> customerContacts = (await connection.QueryAsync<CustomerContactResponse>(sql, request)).AsList();

        return customerContacts;
    }
}
