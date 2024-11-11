using System.Data.Common;
using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Application.Pagination;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProductPrice;
using Dapper;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetMyCustomerProducts;
internal sealed class GetMyCustomerProductsQueryHandler(
    IDbConnectionFactory dbConnectionFactory,
    IDateTimeProvider dateTimeProvider)
    : IQueryHandler<GetMyCustomerProductsQuery, CustomerProductsResponse>
{
    public async Task<Result<CustomerProductsResponse>> Handle(GetMyCustomerProductsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string sortOrder = request.Order.Equals("ASC", StringComparison.OrdinalIgnoreCase) ? "ASC" : "DESC";
        string orderByClause = request.Sort switch
        {
            "productName" => "p.name",
            "productSku" => "p.sku",
            _ => "p.sku" // Default sorting
        };

        string sql = $@"
            WITH FilteredCustomerProducts AS (
                SELECT
                    cp.id AS {nameof(CustomerProduct.Id)},
                    c.id AS {nameof(CustomerProduct.CustomerId)},
                    cp.product_id AS {nameof(CustomerProduct.ProductId)},
                    p.name AS {nameof(CustomerProduct.ProductName)},
                    p.sku AS {nameof(CustomerProduct.ProductSku)},
                    p.unit_of_measure_name AS {nameof(CustomerProduct.UnitOfMeasureName)},
                    ca.name AS {nameof(CustomerProduct.CategoryName)},
                    b.name AS {nameof(CustomerProduct.BrandName)},
                    pg.name AS {nameof(CustomerProduct.ProductGroupName)},
                    pp.sale_price AS {nameof(CustomerProduct.UnitPrice)},
					c.no_discount_fixed_price,
					c.discount,
                    CASE 
                        WHEN cp.fixed_price IS NOT NULL             
                           AND cp.effective_date <= CAST('{DateOnly.FromDateTime(dateTimeProvider.UtcNow)}' AS DATE)
                           AND (cp.expiry_date > CAST('{DateOnly.FromDateTime(dateTimeProvider.UtcNow)}' AS DATE) OR cp.expiry_date is null)
                           THEN cp.fixed_price
                        ELSE 
                            CASE 
                                WHEN cp.fixed_discount IS NOT NULL 
									AND cp.effective_date <= CAST('{DateOnly.FromDateTime(dateTimeProvider.UtcNow)}' AS DATE)
                           			AND (cp.expiry_date > CAST('{DateOnly.FromDateTime(dateTimeProvider.UtcNow)}' AS DATE) OR cp.expiry_date is null)
									THEN pp.sale_price * (1 - cp.fixed_discount / 100.0)
                                ELSE 
                                    pp.sale_price
                            END
                    END AS UnitFixedPrice,
                    CASE 
                        WHEN (cp.fixed_price > 0 OR cp.fixed_discount > 0)             
                           AND cp.effective_date <= CAST('{DateOnly.FromDateTime(dateTimeProvider.UtcNow)}' AS DATE)
                           AND (cp.expiry_date > CAST('{DateOnly.FromDateTime(dateTimeProvider.UtcNow)}' AS DATE) OR cp.expiry_date is null)
                           THEN TRUE
                        ELSE FALSE
                    END AS IsFixedPrice,
                    ROW_NUMBER() OVER (ORDER BY {orderByClause} {sortOrder}) AS RowNum
                FROM crm.customer_products cp
                INNER JOIN crm.customers c ON cp.customer_id = c.id
                INNER JOIN crm.customer_contacts cc ON cc.customer_id = c.id 
                INNER JOIN crm.products p ON cp.product_id = p.id
                INNER JOIN crm.price_tiers pt ON c.price_tier_id = pt.id
                INNER JOIN crm.product_prices pp ON pp.price_tier_id = pt.id AND pp.product_id = p.id
                INNER JOIN crm.categories ca ON p.category_id = ca.id
                INNER JOIN crm.brands b ON p.brand_id = b.id
                INNER JOIN crm.product_groups pg ON p.product_group_id = pg.id
                WHERE cc.id = @CustomerContactId AND cp.is_active = true AND
                    p.name ILIKE '%' || @Search || '%'
                    OR p.sku ILIKE '%' || @Search || '%'
            )
            SELECT 
                c.{nameof(CustomerProduct.Id)},
                c.{nameof(CustomerProduct.CustomerId)},
                c.{nameof(CustomerProduct.ProductId)},
                c.{nameof(CustomerProduct.ProductName)},
                c.{nameof(CustomerProduct.ProductSku)},
                c.{nameof(CustomerProduct.UnitOfMeasureName)},
                c.{nameof(CustomerProduct.CategoryName)},
                c.{nameof(CustomerProduct.BrandName)},
                c.{nameof(CustomerProduct.ProductGroupName)},
                c.{nameof(CustomerProduct.UnitPrice)},
                CAST(CASE
					WHEN c.no_discount_fixed_price = true AND IsFixedPrice = true
					THEN 1 
					ELSE (1 - c.discount / 100)
				END * c.UnitFixedPrice AS DECIMAL(10, 2)) AS {nameof(CustomerProduct.FinalUnitPrice)}
            FROM FilteredCustomerProducts c
            WHERE c.RowNum BETWEEN ((@Page) * @Size) + 1 AND (@Page + 1) * @Size
            ORDER BY c.RowNum;

            SELECT COUNT(*)
            FROM crm.customer_products cp
                INNER JOIN crm.customers c ON cp.customer_id = c.id
                INNER JOIN crm.customer_contacts cc ON cc.customer_id = c.id 
                INNER JOIN crm.products p ON cp.product_id = p.id
                INNER JOIN crm.price_tiers pt ON c.price_tier_id = pt.id
                INNER JOIN crm.product_prices pp ON pp.price_tier_id = pt.id AND pp.product_id = p.id
                INNER JOIN crm.categories ca ON p.category_id = ca.id
                INNER JOIN crm.brands b ON p.brand_id = b.id
                INNER JOIN crm.product_groups pg ON p.product_group_id = pg.id
                WHERE cc.id = @CustomerContactId AND cp.is_active = true AND
                    p.name ILIKE '%' || @Search || '%'
                    OR p.sku ILIKE '%' || @Search || '%'
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
