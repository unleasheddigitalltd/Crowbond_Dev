using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Domain.CustomerContacts;
using Dapper;

namespace Crowbond.Modules.CRM.Application.CustomerContacts.GetCustomerContactDetails;

internal sealed class GetCustomerContactDetailsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetCustomerContactDetailsQuery, CustomerContactDetailsResponse>
{
    public async Task<Result<CustomerContactDetailsResponse>> Handle(GetCustomerContactDetailsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(CustomerContactDetailsResponse.Id)},
                 customer_id AS {nameof(CustomerContactDetailsResponse.CustomerId)},
                 first_name AS {nameof(CustomerContactDetailsResponse.FirstName)},
                 last_name AS {nameof(CustomerContactDetailsResponse.LastName)},
                 phone_number AS {nameof(CustomerContactDetailsResponse.PhoneNumber)},
                 mobile AS {nameof(CustomerContactDetailsResponse.Mobile)},
                 username AS {nameof(CustomerContactDetailsResponse.Username)},
                 email AS {nameof(CustomerContactDetailsResponse.Email)},
                 primary AS {nameof(CustomerContactDetailsResponse.Primary)},             
                 receive_invoice AS {nameof(CustomerContactDetailsResponse.ReceiveInvoice)}, 
                 receive_order AS {nameof(CustomerContactDetailsResponse.ReceiveOrder)}, 
                 receive_price_list AS {nameof(CustomerContactDetailsResponse.ReceivePriceList)},
                 is_active AS {nameof(CustomerContactDetailsResponse.IsActive)}
             FROM crm.customer_contacts t
             WHERE id = @CustomerContactId             
             """;

        CustomerContactDetailsResponse? customerContact = await connection.QuerySingleOrDefaultAsync<CustomerContactDetailsResponse>(sql, request);

        if (customerContact is null)
        {
            return Result.Failure<CustomerContactDetailsResponse>(CustomerContactErrors.NotFound(request.CustomerContactId));
        }

        return customerContact;
    }
}
