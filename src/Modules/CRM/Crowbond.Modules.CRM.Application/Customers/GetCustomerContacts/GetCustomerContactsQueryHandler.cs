using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.CRM.Application.Customers.GetCustomerContacts;

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
                 t.mobile AS {nameof(CustomerContactResponse.Mobile)},
                 t.email AS {nameof(CustomerContactResponse.Email)},
                 t.primary AS {nameof(CustomerContactResponse.Primary)},
                 t.receive_invoice AS {nameof(CustomerContactResponse.ReceiveInvoice)},
                 t.receive_order AS {nameof(CustomerContactResponse.ReceiveOrder)},
                 t.receive_price_list AS {nameof(CustomerContactResponse.ReceivePriceList)},
                 t.is_active AS {nameof(CustomerContactResponse.IsActive)}
             FROM crm.customer_contacts t
             INNER JOIN crm.customers c ON c.id = t.customer_id
             WHERE c.id = @CustomerId;
             """;

        List<CustomerContactResponse> customerContacts = (await connection.QueryAsync<CustomerContactResponse>(sql, request)).AsList();

        return customerContacts;
    }
}
