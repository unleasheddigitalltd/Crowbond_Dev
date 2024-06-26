﻿using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Application.Pagination;
using Crowbond.Common.Domain;
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
            "category" => "c.name",
            "batch" => "s.batch_number",
            "unitOfMeasure" => "p.unit_of_measure_name",
            "location" => "l.name",
            "daysInStock" => "s.received_date",
            "sellByDate" => "s.sell_by_date",
            "useByDate" => "s.use_by_date",
            "active" => "p.active",
            _ => "p.sku" // Default sorting
        };

        string sql = $@"WITH FilteredStocks AS (
                    SELECT 
                        s.id            AS {nameof(Stock.Id)},
                        p.sku           AS {nameof(Stock.Sku)},
                        p.name          AS {nameof(Stock.Name)},	
                        c.name          AS {nameof(Stock.Category)},
                        s.batch_number  AS {nameof(Stock.Batch)},
                        p.unit_of_measure_name AS {nameof(Stock.UnitOfMeasure)},
                        l.name          AS {nameof(Stock.Location)},
                        p.reorder_level AS {nameof(Stock.ReorderLevel)},
                        s.sell_by_date  AS {nameof(Stock.SellByDate)},
                        s.use_by_date   AS {nameof(Stock.UseByDate)},
                        p.active        AS {nameof(Stock.Active)},
                        s.current_qty   AS {nameof(Stock.InStock)},
                        s.current_qty   AS {nameof(Stock.Available)},
                        0.00            AS {nameof(Stock.Allocated)},
                        0.00            AS {nameof(Stock.OnHold)},
                        s.received_date,    
                        s.current_qty,
                        ROW_NUMBER() OVER (ORDER BY {orderByClause} {sortOrder}) AS RowNum
                    FROM wms.stocks s
                    INNER JOIN wms.products p ON p.id = s.product_id
                    INNER JOIN wms.categories c ON c.id = p.category_id
                    INNER JOIN wms.locations l ON s.location_id = l.id
                    WHERE
                        p.sku ILIKE '%' || @Search || '%'
                        OR c.name ILIKE '%' || @Search || '%'
                        OR p.name ILIKE '%' || @Search || '%'
                        OR s.batch_number ILIKE '%' || @Search || '%'
                        OR l.name ILIKE '%' || @Search || '%'
                        OR p.unit_of_measure_name ILIKE '%' || @Search || '%'
                )
                SELECT 
                    s.{nameof(Stock.Id)},
                    s.{nameof(Stock.Sku)},
                    s.{nameof(Stock.Name)},	
                    s.{nameof(Stock.Category)},
                    s.{nameof(Stock.Batch)},
                    s.{nameof(Stock.UnitOfMeasure)},
                    s.{nameof(Stock.InStock)},
                    s.{nameof(Stock.Available)},
                    s.{nameof(Stock.Allocated)},
                    s.{nameof(Stock.OnHold)},
                    s.{nameof(Stock.Location)},
                    s.{nameof(Stock.ReorderLevel)},                
                    CAST(DATE_PART('day', CURRENT_DATE - s.received_date) AS INTEGER) AS {nameof(Stock.DaysInStock)},
                    s.{nameof(Stock.SellByDate)},
                    s.{nameof(Stock.UseByDate)},
                    s.{nameof(Stock.Active)}
                FROM FilteredStocks s
                WHERE s.RowNum BETWEEN ((@Page) * @Size) + 1 AND (@Page + 1) * @Size
                ORDER BY s.RowNum;

                SELECT Count(*) 
                    FROM wms.stocks s
                    INNER JOIN wms.products p ON p.id = s.product_id
                    INNER JOIN wms.categories c ON c.id = p.category_id
                    INNER JOIN wms.locations l ON s.location_id = l.id
                    WHERE
                        p.sku ILIKE '%' || @Search || '%'
                        OR c.name ILIKE '%' || @Search || '%'
                        OR p.name ILIKE '%' || @Search || '%'
                        OR s.batch_number ILIKE '%' || @Search || '%'
                        OR l.name ILIKE '%' || @Search || '%'
                        OR p.unit_of_measure_name ILIKE '%' || @Search || '%'
            ";

        SqlMapper.GridReader multi = await connection.QueryMultipleAsync(sql, request);

        var stocks = (await multi.ReadAsync<Stock>()).ToList();
        int totalCount = await multi.ReadSingleAsync<int>();

        int totalPages = (int)Math.Ceiling(totalCount / (double)request.Size);
        int currentPage = request.Page;
        int pageSize = request.Size;
        int startIndex = currentPage * pageSize;
        int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);

        var pagination = new Pagination(totalCount, pageSize, currentPage, totalPages, startIndex, endIndex);
        var response = new StocksResponse(stocks, pagination);

        return Result<StocksResponse>.Success(response);
    }
}
