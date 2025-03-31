using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Domain.CustomerOutlets;
using Dapper;

namespace Crowbond.Modules.CRM.Application.CustomerOutlets.GetCustomerOutletForOrder;

internal sealed class GetCustomerOutletForOrderQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetCustomerOutletForOrderQuery, CustomerOutletForOrderResponse>
{
    public async Task<Result<CustomerOutletForOrderResponse>> Handle(GetCustomerOutletForOrderQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(CustomerOutletForOrderResponse.Id)},
                 customer_id AS {nameof(CustomerOutletForOrderResponse.CustomerId)},
                 location_name AS {nameof(CustomerOutletForOrderResponse.LocationName)},
                 full_name AS {nameof(CustomerOutletForOrderResponse.FullName)},
                 email AS {nameof(CustomerOutletForOrderResponse.Email)},
                 phone_number AS {nameof(CustomerOutletForOrderResponse.PhoneNumber)},
                 mobile AS {nameof(CustomerOutletForOrderResponse.Mobile)},
                 address_line1 AS {nameof(CustomerOutletForOrderResponse.AddressLine1)},
                 address_line2 AS {nameof(CustomerOutletForOrderResponse.AddressLine2)},
                 town_city AS {nameof(CustomerOutletForOrderResponse.TownCity)},
                 county AS {nameof(CustomerOutletForOrderResponse.County)},
                 country AS {nameof(CustomerOutletForOrderResponse.Country)},
                 postal_code AS {nameof(CustomerOutletForOrderResponse.PostalCode)},
                 delivery_note AS {nameof(CustomerOutletForOrderResponse.DeliveryNote)},
                 delivery_time_from AS {nameof(CustomerOutletForOrderResponse.DeliveryTimeFrom)},
                 delivery_time_to AS {nameof(CustomerOutletForOrderResponse.DeliveryTimeTo)},
                 is24hrs_delivery AS {nameof(CustomerOutletForOrderResponse.Is24HrsDelivery)}
             FROM crm.customer_outlets
             WHERE id = @CustomerOutletId AND is_deleted = false AND is_active = true
             """;

        var customerOutlet = await connection.QuerySingleOrDefaultAsync<CustomerOutletForOrderResponse>(sql, request);

        return customerOutlet ?? Result.Failure<CustomerOutletForOrderResponse>(CustomerOutletErrors.NotFound(request.CustomerOutletId));
    }
}
