using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.CRM.Application.Routes.GetRouteCustomerOutlets;

internal sealed class GetRouteCustomerOutletsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetRouteCustomerOutletsQuery, IReadOnlyCollection<CustomerOutletResponse>>
{
    public async Task<Result<IReadOnlyCollection<CustomerOutletResponse>>> Handle(GetRouteCustomerOutletsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT 
                 c.id AS {nameof(CustomerOutletResponse.Id)},
                 c.customer_id AS {nameof(CustomerOutletResponse.CustomerId)},
                 c.location_name AS {nameof(CustomerOutletResponse.LocationName)}, 
                 c.full_name AS {nameof(CustomerOutletResponse.FullName)}, 
                 c.email AS {nameof(CustomerOutletResponse.Email)}, 
                 c.phone_number AS {nameof(CustomerOutletResponse.PhoneNumber)}, 
                 c.mobile AS {nameof(CustomerOutletResponse.Mobile)}, 
                 c.address_line1 AS {nameof(CustomerOutletResponse.AddressLine1)}, 
                 c.address_line2 AS {nameof(CustomerOutletResponse.AddressLine2)}, 
                 c.town_city AS {nameof(CustomerOutletResponse.TownCity)}, 
                 c.county AS {nameof(CustomerOutletResponse.County)}, 
                 c.country AS {nameof(CustomerOutletResponse.Country)}, 
                 c.postal_code AS {nameof(CustomerOutletResponse.PostalCode)}, 
                 c.delivery_note AS {nameof(CustomerOutletResponse.DeliveryNote)}, 
                 c.delivery_time_from AS {nameof(CustomerOutletResponse.DeliveryTimeFrom)}, 
                 c.delivery_time_to AS {nameof(CustomerOutletResponse.DeliveryTimeTo)}, 
                 c.is24hrs_delivery AS {nameof(CustomerOutletResponse.Is24HrsDelivery)}
             FROM crm.customer_outlet_routes cr
             INNER JOIN crm.customer_outlets c ON c.id = cr.customer_outlet_id 
             INNER JOIN crm.routes r ON r.id = cr.route_id
             WHERE r.id = @RouteId
                 AND c.is_deleted = false
             GROUP BY 
                 c.id, 
                 c.customer_id, 
                 c.location_name, 
                 c.full_name, 
                 c.email, 
                 c.phone_number, 
                 c.mobile, 
                 c.address_line1, 
                 c.address_line2, 
                 c.town_city, 
                 c.county, 
                 c.country, 
                 c.postal_code, 
                 c.delivery_note, 
                 c.delivery_time_from, 
                 c.delivery_time_to, 
                 c.is24hrs_delivery;
             
             SELECT
                 cr.customer_outlet_id AS {nameof(CustomerOutletRouteResponse.CustomerOutletId)},
                 cr.route_id AS {nameof(CustomerOutletRouteResponse.RouteId)},
                 r.name AS {nameof(CustomerOutletRouteResponse.RouteName)},
                 cr.weekday AS {nameof(CustomerOutletRouteResponse.Weekday)}
             FROM crm.customer_outlet_routes cr
             INNER JOIN crm.routes r ON r.id = cr.route_id
             WHERE r.id = @RouteId
             ORDER BY cr.weekday ASC;
             """;

        SqlMapper.GridReader multi = await connection.QueryMultipleAsync(sql, request);

        var customerOutlets = (await multi.ReadAsync<CustomerOutletResponse>()).ToList();
        var customerOutletRoutes = (await multi.ReadAsync<CustomerOutletRouteResponse>()).ToList();


        foreach (CustomerOutletResponse customerOutlet in customerOutlets)
        {
            customerOutlet.Routes = customerOutletRoutes.Where(a => a.CustomerOutletId == customerOutlet.Id).ToList();
        }

        return customerOutlets;
    }
}
