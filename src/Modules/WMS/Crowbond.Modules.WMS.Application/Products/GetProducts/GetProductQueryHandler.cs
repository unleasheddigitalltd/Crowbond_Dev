using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Products.GetProducts.Dto;
using Dapper;
using Product = Crowbond.Modules.WMS.Application.Products.GetProducts.Dto.Product;

namespace Crowbond.Modules.WMS.Application.Products.GetProducts;

internal sealed class GetProductQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetProductQuery, ProductsResponse>
{
    public async Task<Result<ProductsResponse>> Handle(
        GetProductQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string sortOrder = request.Order.Equals("ASC", StringComparison.OrdinalIgnoreCase) ? "ASC" : "DESC";
        string orderByClause = request.Sort switch
        {
            "id" => "p.id",
            "sku" => "p.sku",
            "name" => "p.name",
            "category" => "c.name",
            "unitOfMeasure" => "p.unit_of_measure_name",
            "active" => "p.active",
            _ => "p.name" // Default sorting
        };

        string sql = $@"
            WITH FilteredProducts AS (
                SELECT
                    p.id                            AS {nameof(Product.Id)},
                    p.sku                           AS {nameof(Product.Sku)},
                    p.name                          AS {nameof(Product.Name)},
                    p.unit_of_measure_name          AS {nameof(Product.UnitOfMeasure)},
                    c.name                          AS {nameof(Product.Category)},
                    p.reorder_level                 AS {nameof(Product.ReorderLevel)},
                    p.active                        AS {nameof(Product.Active)},
                    ROW_NUMBER() OVER (ORDER BY {orderByClause} {sortOrder}) AS RowNum
                FROM wms.products p
                INNER JOIN wms.categories c ON p.category_id = c.id
                WHERE
                    p.name ILIKE '%' || @Search || '%'
                    OR p.sku ILIKE '%' || @Search || '%'
                    OR c.name ILIKE '%' || @Search || '%'
                    OR p.unit_of_measure_name ILIKE '%' || @Search || '%'
            )
            SELECT 
                p.{nameof(Product.Id)},
                p.{nameof(Product.Sku)},
                p.{nameof(Product.Name)},
                p.{nameof(Product.UnitOfMeasure)},
                p.{nameof(Product.Category)},
                COALESCE(SUM(s.current_qty), 0) AS {nameof(Product.Stock)},
                p.{nameof(Product.ReorderLevel)},
                p.{nameof(Product.Active)}
            FROM FilteredProducts p
            LEFT OUTER JOIN wms.stocks s ON p.id = s.product_id
            WHERE p.RowNum BETWEEN ((@Page) * @Size) + 1 AND (@Page + 1) * @Size
            GROUP BY 
                p.{nameof(Product.Id)},
                p.{nameof(Product.Sku)},
                p.{nameof(Product.Name)},
                p.{nameof(Product.UnitOfMeasure)},
                p.{nameof(Product.Category)},
                p.{nameof(Product.ReorderLevel)},
                p.{nameof(Product.Active)},
				p.RowNum
            ORDER BY p.RowNum;

            SELECT Count(*) 
                FROM wms.products p
                INNER JOIN wms.categories c ON p.category_id = c.id
                WHERE
                    p.name ILIKE '%' || @Search || '%'
                    OR p.sku ILIKE '%' || @Search || '%'
                    OR c.name ILIKE '%' || @Search || '%'
                    OR p.unit_of_measure_name ILIKE '%' || @Search || '%'
        ";

        SqlMapper.GridReader multi = await connection.QueryMultipleAsync(sql, request);

        var products = (await multi.ReadAsync<Product>()).ToList();
        int totalCount = await multi.ReadSingleAsync<int>();

        int totalPages = (int)Math.Ceiling(totalCount / (double)request.Size);
        int currentPage = request.Page;
        int pageSize = request.Size;
        int startIndex = (currentPage - 1) * pageSize;
        int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);

        return new ProductsResponse(products, new Pagination(totalCount, pageSize, currentPage, totalPages, startIndex, endIndex));
    }
}


