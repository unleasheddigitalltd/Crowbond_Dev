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
                 account_number AS {nameof(CustomerDetailsResponse.AccountNumber)},
                 business_name AS {nameof(CustomerDetailsResponse.BusinessName)},
                 shipping_address_line1 AS {nameof(CustomerDetailsResponse.ShippingAddressLine1)},
                 shipping_address_line2 AS {nameof(CustomerDetailsResponse.ShippingAddressLine2)},
                 shipping_town_city AS {nameof(CustomerDetailsResponse.ShippingTownCity)},
                 shipping_postal_code AS {nameof(CustomerDetailsResponse.ShippingPostalCode)},
                 customer_contact AS {nameof(CustomerDetailsResponse.CustomerContact)},
                 customer_email AS {nameof(CustomerDetailsResponse.CustomerEmail)},
                 customer_phone AS {nameof(CustomerDetailsResponse.CustomerPhone)},
                 billing_address_line1 AS {nameof(CustomerDetailsResponse.BillingAddressLine1)},
                 billing_address_line2 AS {nameof(CustomerDetailsResponse.BillingAddressLine2)},
                 billing_town_city AS {nameof(CustomerDetailsResponse.BillingTownCity)},
                 billing_postal_code AS {nameof(CustomerDetailsResponse.BillingPostalCode)},
                 payment_terms AS {nameof(CustomerDetailsResponse.PaymentTerms)},
                 customer_notes AS {nameof(CustomerDetailsResponse.CustomerNotes)}                 
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
