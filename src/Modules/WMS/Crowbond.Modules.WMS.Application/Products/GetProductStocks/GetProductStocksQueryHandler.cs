using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Products.GetProductStocks.Dtos;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Products.GetProductStocks;

internal sealed class GetProductStocksQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetProductStocksQuery, IReadOnlyCollection<StockResponse>>
{
    public async Task<Result<IReadOnlyCollection<StockResponse>>> Handle(
        GetProductStocksQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT 
             	s.batch_number AS {nameof(StockResponse.Batch)},
             	s.current_qty AS {nameof(StockResponse.InStock)},
             	s.current_qty AS {nameof(StockResponse.Available)},
                0.00 AS  {nameof(StockResponse.Allocated)},
                0.00 AS  {nameof(StockResponse.OnHold)},
             	l.name AS {nameof(StockResponse.Location)},
             	CAST(DATE_PART('day', CURRENT_DATE - s.received_date) AS INTEGER) AS {nameof(StockResponse.DaysInStock)}
             FROM wms.stocks s
             INNER JOIN wms.locations l ON l.id = s.location_id
             WHERE s.product_id = @ProductId;
             """;

        List<StockResponse> stocks = (await connection.QueryAsync<StockResponse>(sql, request)).AsList();

        return stocks;

    }
}
