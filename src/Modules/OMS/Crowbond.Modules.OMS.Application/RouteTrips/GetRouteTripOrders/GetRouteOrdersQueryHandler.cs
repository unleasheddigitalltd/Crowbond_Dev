using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.OMS.Application.RouteTrips.GetRouteTripOrders;

internal sealed class GetRouteOrdersQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetRouteTripOrdersQuery, IReadOnlyCollection<OrderResponse>>
{
    public async Task<Result<IReadOnlyCollection<OrderResponse>>> Handle(GetRouteTripOrdersQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
             $"""
             SELECT
                id AS {nameof(OrderResponse.OrderHeaderId)},
                order_number AS {nameof(OrderResponse.OrderNumber)},
                customer_id AS {nameof(OrderResponse.CustomerId)},
                customer_business_name AS {nameof(OrderResponse.CustomerBusinessName)},
                delivery_location_name AS {nameof(OrderResponse.DeliveryLocationName)},
                delivery_full_name AS {nameof(OrderResponse.DeliveryFullName)},
                delivery_phone AS {nameof(OrderResponse.DeliveryPhone)},
                delivery_mobile AS {nameof(OrderResponse.DeliveryMobile)},
                delivery_notes AS {nameof(OrderResponse.DeliveryNotes)},
                delivery_address_line1 AS {nameof(OrderResponse.DeliveryAddressLine1)},
                delivery_address_line2 AS {nameof(OrderResponse.DeliveryAddressLine2)},
                delivery_town_city AS {nameof(OrderResponse.DeliveryTownCity)},
                delivery_county AS {nameof(OrderResponse.DeliveryCounty)},
                delivery_postal_code AS {nameof(OrderResponse.DeliveryPostalCode)},
                shipping_date AS {nameof(OrderResponse.ShippingDate)},
                route_trip_id AS {nameof(OrderResponse.RouteTripId)},
                route_name AS {nameof(OrderResponse.RouteName)},
                order_amount AS {nameof(OrderResponse.OrderAmount)},
                payment_method AS {nameof(OrderResponse.PaymentMethod)},
                customer_comment AS {nameof(OrderResponse.CustomerComment)}         
             FROM oms.order_headers
             WHERE route_trip_id = @RouteTripId;

             SELECT
                l.id AS {nameof(OrderLineResponse.OrderLineId)},
                l.order_id {nameof(OrderLineResponse.OrderHeaderId)},
                l.product_id AS {nameof(OrderLineResponse.ProductId)},
                l.product_sku AS {nameof(OrderLineResponse.ProductSku)},
                l.product_name AS {nameof(OrderLineResponse.ProductName)},
                l.qty AS {nameof(OrderLineResponse.Qty)}
             FROM oms.order_lines l
             INNER JOIN oms.order_headers h ON h.id = l.order_id
             WHERE h.route_trip_id = @RouteTripId;
             """;

        SqlMapper.GridReader multi = await connection.QueryMultipleAsync(sql, request);

        var orders = (await multi.ReadAsync<OrderResponse>()).ToList();
        var orderLines = (await multi.ReadAsync<OrderLineResponse>()).ToList();

        foreach (OrderResponse? order in orders)
        {
            order.OrderLines = orderLines.Where(a => a.OrderHeaderId == order.OrderHeaderId).ToList();
        }

        return orders;
    }
}
