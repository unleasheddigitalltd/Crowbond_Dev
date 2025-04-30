using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Domain.PurchaseOrders;
using Crowbond.Modules.WMS.Application.Stocks.GetProductStockSummary;
using Dapper;
using MediatR;

namespace Crowbond.Modules.OMS.Application.Reports.GetStockBuyersReport;

internal sealed class GetStockBuyersReportQueryHandler(
    IDbConnectionFactory dbConnectionFactory,
    ISender mediator)
    : IQueryHandler<GetStockBuyersReportQuery, StockBuyersReportResponse>
{
    public async Task<Result<StockBuyersReportResponse>> Handle(GetStockBuyersReportQuery request, CancellationToken cancellationToken)
    {
        await using var connection = await dbConnectionFactory.OpenConnectionAsync();

        // Get all order lines from accepted orders
        var orderLineItems = await connection.QueryAsync<OrderLineItem>(
            """
            SELECT 
                ol.product_id AS ProductId,
                ol.product_name AS ProductName,
                ol.product_sku AS ProductSku,
                SUM(ol.ordered_qty) AS OrderedQuantity
            FROM oms.order_lines ol
            JOIN oms.order_headers oh ON ol.order_header_id = oh.id
            WHERE oh.status = @Status
            GROUP BY ol.product_id, ol.product_name, ol.product_sku
            """,
            new { Status = (int)OrderStatus.Accepted });

        var reportItems = new List<StockBuyersReportItem>();

        foreach (var item in orderLineItems)
        {
            // Get product pack size
            var product = await connection.QuerySingleOrDefaultAsync<ProductInfo>(
                """
                SELECT pack_size AS PackSize
                FROM wms.products
                WHERE id = @ProductId
                """,
                new { item.ProductId });

            // Get current stock quantity
            var stockResult = await mediator.Send(
                new GetProductStockSummaryQuery(item.ProductId),
                cancellationToken);

            var inStockQuantity = stockResult.IsSuccess ? stockResult.Value.Qty : 0;
            var packSize = product?.PackSize;

            // Get purchase order quantities for this product (from pending and approved POs)

            var poQuantity = await connection.QuerySingleOrDefaultAsync<decimal?>(
                """
                SELECT 
                    SUM(pol.qty) AS Quantity
                FROM oms.purchase_order_lines pol
                JOIN oms.purchase_order_headers poh ON pol.purchase_order_header_id = poh.id
                WHERE pol.product_id = @ProductId
                AND poh.status IN (@PendingStatus, @ApprovedStatus)
                """,
                new { 
                    item.ProductId, 
                    PendingStatus = (int)PurchaseOrderStatus.Pending,
                    ApprovedStatus = (int)PurchaseOrderStatus.Approved
                }) ?? 0m;

            // Calculate needed packs and units, taking into account purchase order quantity
            var neededPacks = 0;
            decimal neededUnits = 0;

            if (item.OrderedQuantity > inStockQuantity)
            {
                // Calculate shortage after considering both in-stock and on-order quantities
                var totalAvailableQuantity = inStockQuantity + poQuantity;
                var shortageQuantity = item.OrderedQuantity > totalAvailableQuantity ? 
                    item.OrderedQuantity - totalAvailableQuantity : 0;
                
                if (shortageQuantity > 0)
                {
                    if (packSize is > 0)
                    {
                        // Calculate how many packs are needed, rounding up
                        neededPacks = (int)Math.Ceiling(shortageQuantity / packSize.Value);
                        // Calculate how many units are needed
                        neededUnits = shortageQuantity;
                    }
                    else
                    {
                        // If no pack size is defined, treat each unit as its own pack
                        neededPacks = (int)Math.Ceiling(shortageQuantity);
                        neededUnits = shortageQuantity;
                    }
                }
            }

            reportItems.Add(new StockBuyersReportItem(
                item.ProductId,
                item.ProductName,
                item.ProductSku,
                item.OrderedQuantity,
                inStockQuantity,
                packSize,
                neededPacks,
                neededUnits,
                poQuantity));
        }

        return new StockBuyersReportResponse(reportItems);
    }

    private sealed class OrderLineItem
    {
        public Guid ProductId { get; init; } = Guid.Empty;
        public string ProductName { get; init; } = string.Empty;
        public string ProductSku { get; init; } = string.Empty;
        public decimal OrderedQuantity { get; init; } = decimal.Zero;
    }

    private sealed class ProductInfo
    {
        public decimal? PackSize { get; init; } = decimal.Zero;
    }
}
