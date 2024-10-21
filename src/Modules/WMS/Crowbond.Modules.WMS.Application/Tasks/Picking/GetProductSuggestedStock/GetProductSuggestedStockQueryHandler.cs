using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Stocks;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetProductSuggestedStock;

internal sealed class GetProductSuggestedStockQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetProductSuggestedStockQuery, StockResponse>
{
    public async Task<Result<StockResponse>> Handle(GetProductSuggestedStockQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
             	s.id AS {nameof(StockResponse.Id)}, 
             	s.product_id AS {nameof(StockResponse.ProductId)}, 
             	s.location_id AS {nameof(StockResponse.LocationId)},
             	l.name AS {nameof(StockResponse.LocationName)},
             	s.current_qty AS {nameof(StockResponse.CurrentQty)}, 
             	s.batch_number AS {nameof(StockResponse.BatchNumber)}, 
             	s.received_date AS {nameof(StockResponse.ReceivedDate)}, 
             	s.sell_by_date AS {nameof(StockResponse.SellByDate)}, 
             	s.use_by_date AS {nameof(StockResponse.UseByDate)}, 
             	s.note AS {nameof(StockResponse.Note)}
             FROM wms.stocks s
             INNER JOIN wms.locations l ON l.id = s.location_id
             WHERE 
                s.product_id = @ProductId
                AND s.status = 0 
             	AND s.current_qty > 0 
             	AND l.location_type = 0
             ORDER BY s.received_date
             LIMIT 1;
             """;

        StockResponse? stock = await connection.QuerySingleOrDefaultAsync<StockResponse>(sql, request);

        if (stock is null)
        {
            return Result.Failure<StockResponse>(StockErrors.ProductOutOfStock(request.ProductId));
        }

        return stock;
    }
}
