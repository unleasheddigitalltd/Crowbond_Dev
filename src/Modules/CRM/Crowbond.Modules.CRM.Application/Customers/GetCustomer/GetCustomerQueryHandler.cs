using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Domain.Customers;
using Dapper;

namespace Crowbond.Modules.CRM.Application.Customers.GetCustomer;

internal sealed class GetCustomerQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetCustomerQuery, CustomerResponse>
{
    public async Task<Result<CustomerResponse>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             WITH CustomerContacts AS (
                SELECT
                       c.id,
                       c.account_number AS {nameof(CustomerResponse.AccountNumber)}, 
                       c.business_name AS {nameof(CustomerResponse.BusinessName)},
                       c.billing_address_line1 AS {nameof(CustomerResponse.BillingAddressLine1)},
                       c.billing_address_line2 AS {nameof(CustomerResponse.BillingAddressLine2)},
                       c.billing_town_city AS {nameof(CustomerResponse.BillingTownCity)},
                       c.billing_county AS {nameof(CustomerResponse.BillingCounty)},
                       c.billing_country AS {nameof(CustomerResponse.BillingCountry)},
                       c.billing_postal_code {nameof(CustomerResponse.BillingPostalCode)},
                       t.first_name AS {nameof(CustomerResponse.FirstName)},
                       t.last_name AS {nameof(CustomerResponse.LastName)},
                       t.phone_number AS {nameof(CustomerResponse.PhoneNumber)},
                       t.email AS {nameof(CustomerResponse.Email)},
                       c.is_active AS {nameof(CustomerResponse.IsActive)},
                       ROW_NUMBER() OVER (PARTITION BY c.id ORDER BY t.primary DESC) AS FilterRowNum
                   FROM crm.customers c
                   LEFT JOIN crm.customer_contacts t ON c.id = t.customer_id
                WHERE c.id = @CustomerId
             )
             SELECT * 
             FROM CustomerContacts
             WHERE FilterRowNum = 1
             """;

        CustomerResponse? customer = await connection.QuerySingleOrDefaultAsync<CustomerResponse>(sql, request);

        if (customer is null)
        {
            return Result.Failure<CustomerResponse>(CustomerErrors.NotFound(request.CustomerId));
        }

        return customer;
    }
}
