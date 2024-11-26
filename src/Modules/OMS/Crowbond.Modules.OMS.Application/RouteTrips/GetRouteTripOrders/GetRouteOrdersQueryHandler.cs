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
                o.id AS {nameof(OrderResponse.Id)},
                o.order_no AS {nameof(OrderResponse.OrderNo)},
                o.customer_id AS {nameof(OrderResponse.CustomerId)},
                o.customer_business_name AS {nameof(OrderResponse.CustomerBusinessName)},
                o.delivery_location_name AS {nameof(OrderResponse.DeliveryLocationName)},
                o.delivery_full_name AS {nameof(OrderResponse.DeliveryFullName)},
                o.delivery_phone AS {nameof(OrderResponse.DeliveryPhone)},
                o.delivery_mobile AS {nameof(OrderResponse.DeliveryMobile)},
                o.delivery_notes AS {nameof(OrderResponse.DeliveryNotes)},
                o.delivery_address_line1 AS {nameof(OrderResponse.DeliveryAddressLine1)},
                o.delivery_address_line2 AS {nameof(OrderResponse.DeliveryAddressLine2)},
                o.delivery_town_city AS {nameof(OrderResponse.DeliveryTownCity)},
                o.delivery_county AS {nameof(OrderResponse.DeliveryCounty)},
                o.delivery_postal_code AS {nameof(OrderResponse.DeliveryPostalCode)},
                o.shipping_date AS {nameof(OrderResponse.ShippingDate)},
                o.route_trip_id AS {nameof(OrderResponse.RouteTripId)},
                o.route_name AS {nameof(OrderResponse.RouteName)},
                o.order_amount AS {nameof(OrderResponse.OrderAmount)},
                o.payment_method AS {nameof(OrderResponse.PaymentMethod)},
                o.customer_comment AS {nameof(OrderResponse.CustomerComment)}         
             FROM oms.order_headers o
             INNER JOIN oms.route_trips rt ON rt.id = o.route_trip_id
             WHERE route_trip_id = @RouteTripId;

             SELECT
                l.id AS {nameof(OrderLineResponse.OrderLineId)},
                l.order_header_id {nameof(OrderLineResponse.OrderHeaderId)},
                l.product_id AS {nameof(OrderLineResponse.ProductId)},
                l.product_sku AS {nameof(OrderLineResponse.ProductSku)},
                l.product_name AS {nameof(OrderLineResponse.ProductName)},
                l.ordered_qty AS {nameof(OrderLineResponse.OrderedQty)},
                l.actual_qty AS {nameof(OrderLineResponse.ActualQty)},
                l.delivered_qty AS {nameof(OrderLineResponse.DeliveredQty)},
                l.status AS {nameof(OrderLineResponse.Status)}
             FROM oms.order_lines l
             INNER JOIN oms.order_headers h ON h.id = l.order_header_id
             WHERE h.route_trip_id = @RouteTripId;
             """;
        SqlMapper.GridReader multi = await connection.QueryMultipleAsync(sql, request);

        var orders = (await multi.ReadAsync<OrderResponse>()).ToList();
        var orderLines = (await multi.ReadAsync<OrderLineResponse>()).ToList();

        foreach (OrderResponse? order in orders)
        {
            order.OrderLines = orderLines.Where(a => a.OrderHeaderId == order.Id).ToList();
        }

        return orders;
    }
}


