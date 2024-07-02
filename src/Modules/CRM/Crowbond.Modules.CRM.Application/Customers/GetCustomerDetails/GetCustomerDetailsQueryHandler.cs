using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Domain.Customers;
using Dapper;

namespace Crowbond.Modules.CRM.Application.Customers.GetCustomerDetails;

internal sealed class GetCustomerDetailsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetCustomerDetailsQuery, CustomerDetailsResponse>
{
    public async Task<Result<CustomerDetailsResponse>> Handle(GetCustomerDetailsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(CustomerDetailsResponse.Id)},
                 businessname AS {nameof(CustomerDetailsResponse.BusinessName)},
                 accountnumber AS {nameof(CustomerDetailsResponse.AccountNumber)},
                 shippingaddressline1 AS {nameof(CustomerDetailsResponse.ShippingAddressLine1)},
                 shippingaddressline2 AS {nameof(CustomerDetailsResponse.ShippingAddressLine2)},
                 shippingtowncity AS {nameof(CustomerDetailsResponse.ShippingTownCity)},
                 shippingpostalcode AS {nameof(CustomerDetailsResponse.ShippingPostalCode)},
                 customeremail AS {nameof(CustomerDetailsResponse.CustomerEmail)},
                 customerphone AS {nameof(CustomerDetailsResponse.CustomerPhone)},
                 billingaddressline1 AS {nameof(CustomerDetailsResponse.BillingAddressLine1)},
                 billingaddressline2 AS {nameof(CustomerDetailsResponse.BillingAddressLine2)},
                 billingtowncity AS {nameof(CustomerDetailsResponse.BillingTownCity)},
                 billingpostalcode AS {nameof(CustomerDetailsResponse.BillingPostalCode)},
                 paymentterms AS {nameof(CustomerDetailsResponse.PaymentTerms)},
                 customernotes AS {nameof(CustomerDetailsResponse.CustomerNotes)}
                 
             FROM crm.customers
             WHERE id = @CustomerId
             """;

        CustomerDetailsResponse? customer = await connection.QuerySingleOrDefaultAsync<CustomerDetailsResponse>(sql, request);

        if (customer is null)
        {
            return Result.Failure<CustomerDetailsResponse>(CustomerErrors.NotFound(request.CustomerId));
        }

        return customer;
    }
}
