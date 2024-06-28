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

        const string sql =
            $"""
             SELECT
                id AS {nameof(StockDetailsResponse.Id)},
                original_qty AS {nameof(StockDetailsResponse.OriginalQty)},
                current_qty AS {nameof(StockDetailsResponse.CurrentQty)},
                location_id AS {nameof(StockDetailsResponse.Location)}
             FROM wms.stocks
             WHERE id = @StockId
             """;

        StockDetailsResponse? stock = await connection.QuerySingleOrDefaultAsync<StockDetailsResponse>(sql, request);

        if (stock is null)
        {
            return Result.Failure<StockDetailsResponse>(StockErrors.NotFound(request.StockId));
        }

        return stock;
    }
}
