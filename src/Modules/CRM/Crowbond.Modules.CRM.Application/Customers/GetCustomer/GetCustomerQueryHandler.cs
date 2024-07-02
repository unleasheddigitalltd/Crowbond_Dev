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
                 businessname AS {nameof(CustomerResponse.BusinessName)},
                 accountnumber AS {nameof(CustomerResponse.AccountNumber)},
                 shippingaddressline1 AS {nameof(CustomerResponse.ShippingAddressLine1)},
                 shippingaddressline2 AS {nameof(CustomerResponse.ShippingAddressLine2)},
                 shippingtowncity AS {nameof(CustomerResponse.ShippingTownCity)},
                 shippingpostalcode AS {nameof(CustomerResponse.ShippingPostalCode)},
                 customeremail AS {nameof(CustomerResponse.CustomerEmail)},
                 customerphone AS {nameof(CustomerResponse.CustomerPhone)},
                 billingaddressline1 AS {nameof(CustomerResponse.BillingAddressLine1)},
                 billingaddressline2 AS {nameof(CustomerResponse.BillingAddressLine2)},
                 billingtowncity AS {nameof(CustomerResponse.BillingTownCity)},
                 billingpostalcode AS {nameof(CustomerResponse.BillingPostalCode)},
                 paymentterms AS {nameof(CustomerResponse.PaymentTerms)},
                 customernotes AS {nameof(CustomerResponse.CustomerNotes)}
                 
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
