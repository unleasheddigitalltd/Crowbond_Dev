using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Stocks;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Stocks.GetStockDetails;

internal sealed class GetStockDetailsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetStockDetailsQuery, StockDetailsResponse>
{
    public async Task<Result<StockDetailsResponse>> Handle(GetStockDetailsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string[] caseClauses = Enum.GetValues(typeof(StockStatus))
                          .Cast<StockStatus>()
                          .Select(status => $"WHEN {(int)status} THEN '{status}'")
                          .ToArray();

        string sql =
            $"""
             SELECT
                s.id AS {nameof(StockDetailsResponse.Id)},             
                p.unit_of_measure_name AS {nameof(StockDetailsResponse.UnitOfMeasure)},
                s.original_qty AS {nameof(StockDetailsResponse.OriginalQty)},
                s.current_qty AS {nameof(StockDetailsResponse.CurrentQty)},
                s.current_qty AS {nameof(StockDetailsResponse.Available)},
                0.00 AS {nameof(StockDetailsResponse.Allocated)},
                0.00 AS {nameof(StockDetailsResponse.OnHold)},
                p.reorder_level AS {nameof(StockDetailsResponse.ReorderLevel)},
                s.location_id AS {nameof(StockDetailsResponse.Location)},
                s.received_date AS {nameof(StockDetailsResponse.ReceivedDate)},
                s.sell_by_date AS {nameof(StockDetailsResponse.SellByDate)},
                s.use_by_date AS {nameof(StockDetailsResponse.UseByDate)},
                s.note AS {nameof(StockDetailsResponse.Note)},
                CASE status {string.Join(" ", caseClauses)} ELSE 'Unknown' END AS {nameof(StockDetailsResponse.Status)}
             FROM wms.stocks s
             INNER JOIN wms.products p ON p.id = s.product_id
             WHERE s.id = @StockId
             """;

        StockDetailsResponse? stock = await connection.QuerySingleOrDefaultAsync<StockDetailsResponse>(sql, request);

        if (stock is null)
        {
            return Result.Failure<StockDetailsResponse>(StockErrors.NotFound(request.StockId));
        }

        return stock;
    }
}
