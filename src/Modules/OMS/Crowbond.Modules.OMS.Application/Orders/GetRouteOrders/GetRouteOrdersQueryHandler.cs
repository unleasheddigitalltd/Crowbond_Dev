using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.OMS.Application.Orders.GetRouteOrders;

internal sealed class GetRouteOrdersQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetRouteOrdersQuery, IReadOnlyCollection<OrderResponse>>
{
    public async Task<Result<IReadOnlyCollection<OrderResponse>>> Handle(GetRouteOrdersQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
             $"""
             SELECT
                h.id AS {nameof(OrderResponse.OrderHeaderId)},
                h.invoice_no AS {nameof(OrderResponse.InvoiceNo)},
                h.order_amount AS {nameof(OrderResponse.OrderAmount)},
                h.customer_id AS {nameof(OrderResponse.CustomerId)},
                h.customer_name AS {nameof(OrderResponse.CustomerName)},
                h.customer_mobile AS {nameof(OrderResponse.CustomerMobile)},
                h.customer_email AS {nameof(OrderResponse.CustomerEmail)},
                h.route_id AS {nameof(OrderResponse.ReouteId)},
                h.delivery_address_line1 AS {nameof(OrderResponse.DeliveryAddressLine1)},
                h.delivery_address_line2 AS {nameof(OrderResponse.DeliveryAddressLine2)},
                h.delivery_address_country AS {nameof(OrderResponse.DeliveryAddressCountry)},
                h.delivery_date AS {nameof(OrderResponse.DeliveryDate)},
                h.delivery_address_postal_code AS {nameof(OrderResponse.DeliveryAddressPostalCode)},
                h.delivery_address_town_city AS {nameof(OrderResponse.DeliveryAddressTownCity)},
                h.sales_order_number AS {nameof(OrderResponse.
                OrderNumber)},
                h.shipping_address_company AS {nameof(OrderResponse.DeliveryAddressCompany)}                
             FROM oms.order_headers h
             WHERE h.route_id = @RouteId;

             SELECT
                l.id AS {nameof(OrderLineResponse.OrderLineId)},
                l.order_id {nameof(OrderLineResponse.OrderHeaderId)},
                l.product_id AS {nameof(OrderLineResponse.ProductId)},
                l.product_sku AS {nameof(OrderLineResponse.ProductSku)},
                l.product_name AS {nameof(OrderLineResponse.ProductName)},
                l.qty AS {nameof(OrderLineResponse.Qty)}
             FROM oms.order_lines l
             INNER JOIN oms.order_headers h ON h.id = l.order_id
             WHERE h.route_id = @RouteId;
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
