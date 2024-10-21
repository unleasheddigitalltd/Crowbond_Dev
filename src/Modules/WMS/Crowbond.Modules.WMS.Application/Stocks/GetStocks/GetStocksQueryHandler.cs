using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Application.Pagination;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Stocks;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Stocks.GetStocks;

internal sealed class GetStocksQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetStocksQuery, StocksResponse>
{
    public async Task<Result<StocksResponse>> Handle(GetStocksQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string sortOrder = request.Order.Equals("ASC", StringComparison.OrdinalIgnoreCase) ? "ASC" : "DESC";
        string orderByClause = request.Sort switch
        {
            "sku" => "p.sku",
            "productDescription" => "p.name",
            "categoryName" => "c.name",
            "batch" => "s.batch_number",
            "unitOfMeasureName" => "p.unit_of_measure_name",
            "location" => "l.name",
            "daysInStock" => "s.received_date",
            "sellByDate" => "s.sell_by_date",
            "useByDate" => "s.use_by_date",
            "isActive" => "p.is_active",
            "status" => "s.status",
            _ => "p.sku" // Default sorting
        };

        string[] caseClauses = Enum.GetValues(typeof(StockStatus))
                          .Cast<StockStatus>()
                          .Select(status => $"WHEN {(int)status} THEN '{status}'")
                          .ToArray();

        string sql = $@"WITH FilteredStocks AS (
                    SELECT 
                        s.id AS {nameof(Stock.Id)},
                        p.sku AS {nameof(Stock.Sku)},
                        p.name AS {nameof(Stock.Name)},	
                        c.name AS {nameof(Stock.CategoryName)},
                        s.batch_number AS {nameof(Stock.Batch)},
                        p.unit_of_measure_name AS {nameof(Stock.UnitOfMeasureName)},
                        l.name AS {nameof(Stock.Location)},
                        p.is_active AS {nameof(Stock.IsActive)},
                        CASE s.status {string.Join(" ", caseClauses)} ELSE 'Unknown' END AS {nameof(Stock.Status)},
                        s.current_qty AS {nameof(Stock.InStock)},
                        s.received_date,    
                        s.current_qty,
                        ROW_NUMBER() OVER (ORDER BY {orderByClause} {sortOrder}) AS RowNum
                    FROM wms.stocks s
                    INNER JOIN wms.products p ON p.id = s.product_id
                    INNER JOIN crm.categories c ON p.category_id = c.id
                    INNER JOIN wms.locations l ON s.location_id = l.id
                    WHERE
                        s.current_qty != 0 AND
                        (p.sku ILIKE '%' || @Search || '%'
                        OR c.name ILIKE '%' || @Search || '%'
                        OR p.name ILIKE '%' || @Search || '%'
                        OR s.batch_number ILIKE '%' || @Search || '%'
                        OR l.name ILIKE '%' || @Search || '%'
                        OR p.unit_of_measure_name ILIKE '%' || @Search || '%')
                )
                SELECT 
                    s.{nameof(Stock.Id)},
                    s.{nameof(Stock.Sku)},
                    s.{nameof(Stock.Name)},	
                    s.{nameof(Stock.CategoryName)},
                    s.{nameof(Stock.Batch)},
                    s.{nameof(Stock.UnitOfMeasureName)},
                    s.{nameof(Stock.InStock)},
                    s.{nameof(Stock.Location)},               
                    CURRENT_DATE - s.received_date AS {nameof(Stock.DaysInStock)},
                    s.{nameof(Stock.Status)},
                    s.{nameof(Stock.IsActive)}
                FROM FilteredStocks s
                WHERE s.RowNum BETWEEN ((@Page) * @Size) + 1 AND (@Page + 1) * @Size
                ORDER BY s.RowNum;

            SELECT Count(*) 
                FROM wms.stocks s
                INNER JOIN wms.products p ON p.id = s.product_id
                INNER JOIN wms.categories c ON c.id = p.category_id
                INNER JOIN wms.locations l ON s.location_id = l.id
                WHERE
                    s.current_qty != 0 AND
                    (p.sku ILIKE '%' || @Search || '%'
                    OR c.name ILIKE '%' || @Search || '%'
                    OR p.name ILIKE '%' || @Search || '%'
                    OR s.batch_number ILIKE '%' || @Search || '%'
                    OR l.name ILIKE '%' || @Search || '%'
                    OR p.unit_of_measure_name ILIKE '%' || @Search || '%')
        ";

        SqlMapper.GridReader multi = await connection.QueryMultipleAsync(sql, request);

        var stocks = (await multi.ReadAsync<Stock>()).ToList();
        int totalCount = await multi.ReadSingleAsync<int>();

        int totalPages = (int)Math.Ceiling(totalCount / (double)request.Size);
        int currentPage = request.Page;
        int pageSize = request.Size;
        int startIndex = (currentPage - 1) * pageSize;
        int endIndex = totalCount == 0 ? 0 : Math.Min(startIndex + pageSize - 1, totalCount - 1);

        return new StocksResponse(stocks, new Pagination(totalCount, pageSize, currentPage, totalPages, startIndex, endIndex));
    }
}
