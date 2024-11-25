using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.OMS.Application.Orders.GetOrderLines;

internal sealed class GetOrderLinesQueryHandle(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetOrderLinesQuery, IReadOnlyCollection<OrderLineResponse>>
{
    public async Task<Result<IReadOnlyCollection<OrderLineResponse>>> Handle(GetOrderLinesQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 ol.id AS {nameof(OrderLineResponse.OrderLineId)},
                 ol.order_header_id AS {nameof(OrderLineResponse.OrderHeaderId)},
                 ol.product_id AS {nameof(OrderLineResponse.ProductId)},
                 ol.product_sku AS {nameof(OrderLineResponse.ProductSku)},
                 ol.product_name AS {nameof(OrderLineResponse.ProductName)},
                 ol.unit_of_measure_name AS {nameof(OrderLineResponse.UnitOfMeasureName)},
                 ol.unit_price AS {nameof(OrderLineResponse.UnitPrice)},
                 ol.ordered_qty AS {nameof(OrderLineResponse.OrderedQty)},
                 ol.actual_qty AS {nameof(OrderLineResponse.ActualQty)},
                 ol.delivered_qty AS {nameof(OrderLineResponse.DeliveredQty)},
                 ol.sub_total AS {nameof(OrderLineResponse.SubTotal)},
                 ol.tax AS {nameof(OrderLineResponse.Tax)},
                 ol.line_total AS {nameof(OrderLineResponse.LineTotal)},
                 ol.deduction_sub_total AS {nameof(OrderLineResponse.DeductionSubTotal)},
                 ol.deduction_tax AS {nameof(OrderLineResponse.DeductionTax)},
                 ol.deduction_line_total AS {nameof(OrderLineResponse.DeductionLineTotal)},
                 ol.tax_rate_type AS {nameof(OrderLineResponse.TaxRateType)},
                 ol.status AS {nameof(OrderLineResponse.LineStatus)}
             FROM oms.order_lines ol
             WHERE ol.order_header_id = @OrderHeaderId
             """;

        List<OrderLineResponse> orderLines = (await connection.QueryAsync<OrderLineResponse>(sql, request)).AsList();

        return orderLines;
    }
}
