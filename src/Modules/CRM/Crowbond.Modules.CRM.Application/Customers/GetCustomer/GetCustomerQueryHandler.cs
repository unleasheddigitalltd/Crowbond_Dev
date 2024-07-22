using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Customers.GetCustomerDetails;
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
             SELECT
                 id AS {nameof(CustomerResponse.Id)},
                 business_name AS {nameof(CustomerResponse.BusinessName)},
                 account_number AS {nameof(CustomerResponse.AccountNumber)},
                 billing_address_line1 AS {nameof(CustomerResponse.BillingAddressLine1)},
                 billing_address_line2 AS {nameof(CustomerResponse.BillingAddressLine2)},
                 billing_town_city AS {nameof(CustomerResponse.BillingTownCity)},
                 is_active AS {nameof(CustomerResponse.IsActive)}             
             FROM crm.customers
             WHERE id = @CustomerId
             """;

        CustomerResponse? customer = await connection.QuerySingleOrDefaultAsync<CustomerResponse>(sql, request);

        if (customer is null)
        {
            return Result.Failure<CustomerResponse>(CustomerErrors.NotFound(request.CustomerId));
        }

        return customer;
    }
}
