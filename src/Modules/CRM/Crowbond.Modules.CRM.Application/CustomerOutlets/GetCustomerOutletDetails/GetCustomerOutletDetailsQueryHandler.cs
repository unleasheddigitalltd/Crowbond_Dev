using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Customers.GetCustomerDetails;
using Crowbond.Modules.CRM.Domain.CustomerContacts;
using Crowbond.Modules.CRM.Domain.CustomerOutlets;
using Dapper;

namespace Crowbond.Modules.CRM.Application.CustomerOutlets.GetCustomerOutletDetails;

internal sealed class GetCustomerOutletDetailsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetCustomerOutletDetailsQuery, CustomerOutletDetailsResponse>
{
    public async Task<Result<CustomerOutletDetailsResponse>> Handle(GetCustomerOutletDetailsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(CustomerOutletDetailsResponse.Id)},
                 customer_id AS {nameof(CustomerOutletDetailsResponse.CustomerId)},
                 location_name AS {nameof(CustomerOutletDetailsResponse.LocationName)}, 
                 full_name AS {nameof(CustomerOutletDetailsResponse.FullName)}, 
                 email AS {nameof(CustomerOutletDetailsResponse.Email)}, 
                 phone_number AS {nameof(CustomerOutletDetailsResponse.PhoneNumber)}, 
                 mobile AS {nameof(CustomerOutletDetailsResponse.Mobile)}, 
                 address_line1 AS {nameof(CustomerOutletDetailsResponse.AddressLine1)}, 
                 address_line2 AS {nameof(CustomerOutletDetailsResponse.AddressLine2)}, 
                 town_city AS {nameof(CustomerOutletDetailsResponse.TownCity)}, 
                 county AS {nameof(CustomerOutletDetailsResponse.County)}, 
                 country AS {nameof(CustomerOutletDetailsResponse.Country)}, 
                 postal_code AS {nameof(CustomerOutletDetailsResponse.PostalCode)}, 
                 delivery_note AS {nameof(CustomerOutletDetailsResponse.DeliveryNote)}, 
                 delivery_time_from AS {nameof(CustomerOutletDetailsResponse.DeliveryTimeFrom)}, 
                 delivery_time_to AS {nameof(CustomerOutletDetailsResponse.DeliveryTimeTo)}, 
                 is24hrs_delivery AS {nameof(CustomerOutletDetailsResponse.Is24HrsDelivery)}
             FROM crm.customer_outlets
             WHERE id = @CustomerOutletId AND is_deleted = false;
             
             SELECT
                 cr.customer_outlet_id AS {nameof(CustomerOutletRouteResponse.CustomerOutletId)},
                 cr.route_id AS {nameof(CustomerOutletRouteResponse.RouteId)},
                 r.name AS {nameof(CustomerOutletRouteResponse.RouteName)},
                 cr.weekday AS {nameof(CustomerOutletRouteResponse.Weekday)}
             FROM crm.customer_outlet_routes cr
             INNER JOIN crm.routes r ON r.id = cr.route_id
             WHERE cr.customer_outlet_id = @CustomerOutletId
             ORDER BY cr.weekday ASC;
             """;


        SqlMapper.GridReader multi = await connection.QueryMultipleAsync(sql, request);

        var customerOutlets = (await multi.ReadAsync<CustomerOutletDetailsResponse>()).ToList();
        var customerOutletRoutes = (await multi.ReadAsync<CustomerOutletRouteResponse>()).ToList();

        CustomerOutletDetailsResponse? customerOutlet = customerOutlets.SingleOrDefault();

        if (customerOutlet is null)
        {
            return Result.Failure<CustomerOutletDetailsResponse>(CustomerOutletErrors.NotFound(request.CustomerOutletId));
        }

        customerOutlet.Routes = customerOutletRoutes.Where(a => a.CustomerOutletId == customerOutlet.Id).ToList();


        return customerOutlet;
    }
}
