using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Stocks.GetProductStockSummary;

internal sealed class GetProductStockSummaryQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetProductStockSummaryQuery, ProductStockSummaryResponse>
{
    public async Task<Result<ProductStockSummaryResponse>> Handle(GetProductStockSummaryQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string sql =
            $"""
             SELECT
                SUM(current_qty) AS {nameof(ProductStockSummaryResponse.Qty)}
             FROM wms.stocks
             WHERE product_id = @ProductId
             GROUP BY product_id;
             """;

        ProductStockSummaryResponse? response = await connection.QuerySingleOrDefaultAsync<ProductStockSummaryResponse>(sql, request);

        if (response is null)
        {
            return new ProductStockSummaryResponse(0);
        }

        return response;
    }
}
