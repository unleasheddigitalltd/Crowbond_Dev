using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Domain.Customers;
using Dapper;

namespace Crowbond.Modules.CRM.Application.Customers.GetCustomerContact;

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
                 username AS {nameof(CustomerContactResponse.Username)},
                 email AS {nameof(CustomerContactResponse.Email)},
                 first_name AS {nameof(CustomerContactResponse.FirstName)},
                 last_name AS {nameof(CustomerContactResponse.LastName)}
             FROM crm.customer_contacts
             WHERE id = @CustomerContactId
             """;

        CustomerContactResponse? customerContact = await connection.QuerySingleOrDefaultAsync<CustomerContactResponse>(sql, request);

        if (customerContact is null)
        {
            return Result.Failure<CustomerContactResponse>(CustomerErrors.NotFound(request.CustomerContactId));
        }

        return customerContact;
    }
}
