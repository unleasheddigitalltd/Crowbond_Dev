using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Domain.PurchaseOrders;
using Dapper;

namespace Crowbond.Modules.OMS.Application.Orders.GetOrderWithLines;

internal sealed class GetOrderWithLinesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetOrderWithLinesQuery, OrderResponse>
{
    public async Task<Result<OrderResponse>> Handle(GetOrderWithLinesQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 o.id AS {nameof(OrderResponse.Id)},          
                 o.order_no AS {nameof(OrderResponse.OrderNo)},
                 o.customer_business_name AS {nameof(OrderResponse.CustomerBusinessName)},
                 o.route_trip_id AS {nameof(OrderResponse.RouteTripId)},
                 l.id AS {nameof(OrderLineResponse.OrderLineId)},
                 l.order_header_id AS {nameof(OrderLineResponse.OrderHeaderId)},
                 l.product_id AS {nameof(OrderLineResponse.ProductId)},
                 l.ordered_qty AS {nameof(OrderLineResponse.OrderedQty)},
                 l.is_bulk AS {nameof(OrderLineResponse.IsBulk)}
             FROM oms.order_headers o
             INNER JOIN oms.order_lines l ON o.id = l.order_header_id
             WHERE o.id = @OrderHeaderId
             """;

        Dictionary<Guid, OrderResponse> ordersDictionary = [];
        await connection.QueryAsync<OrderResponse, OrderLineResponse, OrderResponse>(
            sql,
            (order, orderLine) =>
            {
                if (ordersDictionary.TryGetValue(order.Id, out OrderResponse? existingEvent))
                {
                    order = existingEvent;
                }
                else
                {
                    ordersDictionary.Add(order.Id, order);
                }

                order.Lines.Add(orderLine);

                return order;
            },
            request,
            splitOn: nameof(OrderLineResponse.OrderLineId));

        if (!ordersDictionary.TryGetValue(request.OrderHeaderId, out OrderResponse order))
        {
            return Result.Failure<OrderResponse>(PurchaseOrderErrors.NotFound(request.OrderHeaderId));
        }

        return order;
    }
}
