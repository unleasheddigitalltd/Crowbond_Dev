using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Domain.Orders;
using Dapper;

namespace Crowbond.Modules.OMS.Application.Orders.GetOrderLine;

internal sealed class GetOrderLineQueryHandler(
    IDbConnectionFactory dbConnectionFactory,
    InventoryService inventoryService)
    : IQueryHandler<GetOrderLineQuery, OrderLineResponse>
{
    public async Task<Result<OrderLineResponse>> Handle(GetOrderLineQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT                 
                 id AS {nameof(OrderLineResponse.OrderLineId)},
                 order_header_id AS {nameof(OrderLineResponse.OrderHeaderId)},
                 product_id AS {nameof(OrderLineResponse.ProductId)},
                 product_sku AS {nameof(OrderLineResponse.ProductSku)},
                 product_name AS {nameof(OrderLineResponse.ProductName)},
                 unit_of_measure_name AS {nameof(OrderLineResponse.UnitOfMeasureName)},
                 unit_price AS {nameof(OrderLineResponse.UnitPrice)},
                 qty AS {nameof(OrderLineResponse.Qty)},
                 sub_total AS {nameof(OrderLineResponse.SubTotal)},
                 tax AS {nameof(OrderLineResponse.Tax)},
                 line_total AS {nameof(OrderLineResponse.LineTotal)},
                 tax_rate_type AS {nameof(OrderLineResponse.TaxRateType)},
                 status AS {nameof(OrderLineResponse.LineStatus)}
             FROM oms.order_lines
             WHERE id = @OrderLineId
             """;

        OrderLineResponse? orderLine = await connection.QuerySingleOrDefaultAsync<OrderLineResponse>(sql, request);

        if (orderLine is null)
        {
            return Result.Failure<OrderLineResponse>(OrderErrors.LineNotFound(request.OrderLineId));
        }

        decimal availableQty = await inventoryService.GetAvailableQuantityAsync(orderLine.ProductId, cancellationToken);
        orderLine.AvailableQty = availableQty;
        

        return orderLine;
    }
}
