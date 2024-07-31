using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Domain.CustomerContacts;
using Dapper;

namespace Crowbond.Modules.CRM.Application.CustomerContacts.GetCustomerContactDetails;

internal sealed class GetCustomerContactDetailsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetCustomerContactDetailsQuery, CustomerContactResponse>
{
    public async Task<Result<CustomerContactResponse>> Handle(GetCustomerContactDetailsQuery request, CancellationToken cancellationToken)
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
                 mobile AS {nameof(CustomerContactResponse.Mobile)},
                 username AS {nameof(CustomerContactResponse.Username)},
                 email AS {nameof(CustomerContactResponse.Email)},
                 primary AS {nameof(CustomerContactResponse.Primary)},             
                 receive_invoice AS {nameof(CustomerContactResponse.ReceiveInvoice)}, 
                 receive_order AS {nameof(CustomerContactResponse.ReceiveOrder)}, 
                 receive_price_list AS {nameof(CustomerContactResponse.ReceivePriceList)},
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
