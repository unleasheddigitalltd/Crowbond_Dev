using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Application.Pagination;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProducts;

internal sealed class GetCustomerProductsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetCustomerProductsQuery, CustomerProductsResponse>
{
    public async Task<Result<CustomerProductsResponse>> Handle(GetCustomerProductsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string sortOrder = request.Order.Equals("ASC", StringComparison.OrdinalIgnoreCase) ? "ASC" : "DESC";
        string orderByClause = request.Sort switch
        {
            "ProductSku" => "p.sku",
            "ProductName" => "p.name",
            "BrandName" => "b.name",
            _ => "p.sku" // Default sorting
        };
        string sql =
            $@"
            WITH FilteredCustomerProducts AS (
                SELECT
                    cp.id AS {nameof(CustomerProduct.Id)},
                    cp.customer_id AS {nameof(CustomerProduct.CustomerId)},
                    cp.product_id AS {nameof(CustomerProduct.ProductId)},
                    p.name AS {nameof(CustomerProduct.ProductName)},
                    p.sku AS {nameof(CustomerProduct.ProductSku)},
                    p.unit_of_measure_name AS {nameof(CustomerProduct.UnitOfMeasureName)},
                    c.name AS {nameof(CustomerProduct.CategoryName)},
                    b.name AS {nameof(CustomerProduct.BrandName)},
                    pg.name AS {nameof(CustomerProduct.ProductGroupName)},
                    cp.fixed_price AS {nameof(CustomerProduct.FixedPrice)},
                    cp.fixed_discount AS {nameof(CustomerProduct.FixedDiscount)},
                    cp.comments AS {nameof(CustomerProduct.Comments)},
                    cp.effective_date AS {nameof(CustomerProduct.EffectiveDate)},
                    cp.expiry_date AS {nameof(CustomerProduct.ExpiryDate)},
                    ROW_NUMBER() OVER (ORDER BY {orderByClause} {sortOrder}) AS RowNum
                FROM crm.customer_products cp
                INNER JOIN crm.products p ON cp.product_id = p.id
                INNER JOIN crm.categories c ON p.category_id = c.id
                INNER JOIN crm.brands b ON p.brand_id = b.id
                INNER JOIN crm.product_groups pg ON p.product_group_id = pg.id
                WHERE cp.customer_id = @CustomerId AND cp.is_active = true
                   AND (p.name ILIKE '%' || @Search || '%'
                       OR p.sku ILIKE '%' || @Search || '%'
                       OR b.name ILIKE '%' || @Search || '%'
                       OR p.unit_of_measure_name ILIKE '%' || @Search || '%')
            )
            SELECT 
                cp.{nameof(CustomerProduct.Id)},
                cp.{nameof(CustomerProduct.CustomerId)},
                cp.{nameof(CustomerProduct.ProductId)},
                cp.{nameof(CustomerProduct.ProductName)},
                cp.{nameof(CustomerProduct.ProductSku)},
                cp.{nameof(CustomerProduct.UnitOfMeasureName)},
                cp.{nameof(CustomerProduct.CategoryName)},
                cp.{nameof(CustomerProduct.BrandName)},
                cp.{nameof(CustomerProduct.ProductGroupName)},
                cp.{nameof(CustomerProduct.FixedPrice)},
                cp.{nameof(CustomerProduct.FixedDiscount)},
                cp.{nameof(CustomerProduct.Comments)},
                cp.{nameof(CustomerProduct.EffectiveDate)},
                cp.{nameof(CustomerProduct.ExpiryDate)}
            FROM FilteredCustomerProducts cp
            WHERE cp.RowNum BETWEEN ((@Page) * @Size) + 1 AND (@Page + 1) * @Size
            ORDER BY cp.RowNum;

            SELECT Count(*)
            FROM crm.customer_products cp
            INNER JOIN crm.products p ON cp.product_id = p.id
            INNER JOIN crm.categories c ON p.category_id = c.id
            INNER JOIN crm.brands b ON p.brand_id = b.id
            INNER JOIN crm.product_groups pg ON p.product_group_id = pg.id
            WHERE cp.customer_id = @CustomerId AND cp.is_active = true
               AND (p.name ILIKE '%' || @Search || '%'
                   OR p.sku ILIKE '%' || @Search || '%'
                   OR b.name ILIKE '%' || @Search || '%'
                   OR p.unit_of_measure_name ILIKE '%' || @Search || '%');
             ";

        SqlMapper.GridReader multi = await connection.QueryMultipleAsync(sql, request);

        var customerProducts = (await multi.ReadAsync<CustomerProduct>()).ToList();
        int totalCount = await multi.ReadSingleAsync<int>();

        int totalPages = (int)Math.Ceiling(totalCount / (double)request.Size);
        int currentPage = request.Page;
        int pageSize = request.Size;
        int startIndex = currentPage * pageSize;
        int endIndex = totalCount == 0 ? 0 : Math.Min(startIndex + pageSize - 1, totalCount - 1);

        return new CustomerProductsResponse(customerProducts, new Pagination(totalCount, pageSize, currentPage, totalPages, startIndex, endIndex));
    }
}
