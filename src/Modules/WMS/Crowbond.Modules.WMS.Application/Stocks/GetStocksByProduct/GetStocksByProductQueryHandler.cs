using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Stocks;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Stocks.GetStocksByProduct;

internal sealed class GetStocksByProductQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetStocksByProductQuery, IReadOnlyCollection<StockResponse>>
{
    public async Task<Result<IReadOnlyCollection<StockResponse>>> Handle(GetStocksByProductQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string[] caseClauses = Enum.GetValues(typeof(StockStatus))
                          .Cast<StockStatus>()
                          .Select(status => $"WHEN {(int)status} THEN '{status}'")
                          .ToArray();

        string sql = $@"SELECT 
                        s.id AS {nameof(StockResponse.Id)},
                        p.sku AS {nameof(StockResponse.Sku)},
                        p.name AS {nameof(StockResponse.Name)},	
                        c.name AS {nameof(StockResponse.CategoryName)},
                        s.batch_number AS {nameof(StockResponse.Batch)},
                        p.unit_of_measure_name AS {nameof(StockResponse.UnitOfMeasureName)},
                        s.current_qty AS {nameof(StockResponse.InStock)},
                        l.name AS {nameof(StockResponse.Location)},
                        CURRENT_DATE - s.received_date AS {nameof(StockResponse.DaysInStock)},
                        p.is_active AS {nameof(StockResponse.IsActive)},
                        CASE s.status {string.Join(" ", caseClauses)} ELSE 'Unknown' END AS {nameof(Stock.Status)}
                    FROM wms.stocks s
                    INNER JOIN wms.products p ON p.id = s.product_id
                    INNER JOIN crm.categories c ON p.category_id = c.id
                    INNER JOIN wms.locations l ON s.location_id = l.id
                    WHERE
                        s.current_qty != 0 AND 
                        p.id = @ProductId";

        List<StockResponse> stocks = (await connection.QueryAsync<StockResponse>(sql, request)).AsList();

        return stocks;
    }
}
