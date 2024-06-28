using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Stocks;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Stocks.GetStock;

internal sealed class GetStockQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetStockQuery, StockResponse>
{
    public async Task<Result<StockResponse>> Handle(GetStockQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT 
                s.id AS {nameof(StockResponse.Id)},
                p.sku AS {nameof(StockResponse.Sku)},
                p.name AS {nameof(StockResponse.Name)},	
                c.name AS {nameof(StockResponse.Category)},
                s.batch_number AS {nameof(StockResponse.Batch)},
                p.unit_of_measure_name AS {nameof(StockResponse.UnitOfMeasure)},
                s.current_qty AS {nameof(StockResponse.InStock)},
                s.current_qty AS {nameof(StockResponse.Available)},
                0.00 AS {nameof(StockResponse.Allocated)},
                0.00 AS {nameof(StockResponse.OnHold)},
                l.name AS {nameof(StockResponse.Location)},
                p.reorder_level AS {nameof(StockResponse.ReorderLevel)},
                CAST(DATE_PART('day', CURRENT_DATE - s.received_date) AS INTEGER) AS {nameof(StockResponse.DaysInStock)},
                s.sell_by_date AS {nameof(StockResponse.SellByDate)},
                s.use_by_date AS {nameof(StockResponse.UseByDate)},
                p.active AS {nameof(StockResponse.Active)}
             FROM wms.stocks s
                INNER JOIN wms.products p ON p.id = s.product_id
                INNER JOIN wms.categories c ON c.id = p.category_id
                INNER JOIN wms.locations l ON s.location_id = l.id
             WHERE s.id = @StockId;
             """;

        StockResponse? stock = await connection.QuerySingleOrDefaultAsync<StockResponse>(sql, request);

        if (stock is null)
        {
            return Result.Failure<StockResponse>(StockErrors.NotFound(request.StockId));
        }

        return stock;
    }
}
