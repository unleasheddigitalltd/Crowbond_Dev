using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Products;
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
                 l.name AS {nameof(StockResponse.Location)},
                 s.original_qty AS {nameof(StockResponse.OriginalQty)},
                 s.current_qty AS {nameof(StockResponse.CurrentQty)}
             FROM wms.stocks s
             INNER JOIN wms.locations l ON l.id = s.location_id             
             """;

        StockResponse? stock = await connection.QuerySingleOrDefaultAsync<StockResponse>(sql, request);

        if (stock is null)
        {
            return Result.Failure<StockResponse>(StockErrors.NotFound(request.StockId));
        }

        return stock;
    }
}
