using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.Products.Application.Products.GetProducts.Dto;
using Dapper;
using Product = Crowbond.Modules.Products.Application.Products.GetProducts.Dto.Product;

namespace Crowbond.Modules.Products.Application.Products.GetProducts;

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
            "id" => "id",
            "sku" => "sku",
            "name" => "name",
            "filterTypeName" => "filter_type_name",
            "unitOfMeasureName" => "unit_of_measure_name",
            "active" => "active",
            _ => "name" // Default sorting
        };

        string sql = $@"
            WITH FilteredProducts AS (
                SELECT *,
                    ROW_NUMBER() OVER (ORDER BY {orderByClause} {sortOrder}) AS RowNum
                FROM products.products
                WHERE
                    name ILIKE '%' || @Search || '%'
                    OR sku ILIKE '%' || @Search || '%'
                    OR filter_type_name ILIKE '%' || @Search || '%'
                    OR unit_of_measure_name ILIKE '%' || @Search || '%'
            )
            SELECT 
                id                     AS {nameof(Product.Id)},
                sku                    AS {nameof(Product.Sku)},
                name                   AS {nameof(Product.Name)},
                filter_type_name       AS {nameof(Product.FilterTypeName)},
                unit_of_measure_name   AS {nameof(Product.UnitOfMeasureName)},
                active                 AS {nameof(Product.Active)}
            FROM FilteredProducts
            WHERE RowNum BETWEEN ((@Page) * @Size) + 1 AND (@Page + 1) * @Size
            ORDER BY RowNum;

            SELECT Count(*) 
                FROM products.products
                WHERE
                    name ILIKE '%' || @Search || '%'
                    OR sku ILIKE '%' || @Search || '%'
                    OR filter_type_name ILIKE '%' || @Search || '%'
                    OR unit_of_measure_name ILIKE '%' || @Search || '%'
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


