using System.Data.Common;
using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProductPrice;
using Crowbond.Modules.CRM.Domain.CustomerProducts;
using Dapper;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProductBySku;


internal sealed class GetCustomerProductBySkuQueryHandler(IDbConnectionFactory dbConnectionFactory, 
    IDateTimeProvider dateTimeProvider)
    : IQueryHandler<GetCustomerProductBySkuQuery, CustomerProductPriceResponse>
{
    
    public async Task<Result<CustomerProductPriceResponse>> Handle(GetCustomerProductBySkuQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string sql =
            $"""
             SELECT
                 COALESCE(cp.id, NULL) AS {nameof(CustomerProductPriceResponse.Id)},
                 c.id AS {nameof(CustomerProductPriceResponse.CustomerId)},
                 p.id AS {nameof(CustomerProductPriceResponse.ProductId)},
                 p.name AS {nameof(CustomerProductPriceResponse.ProductName)},
                 p.sku AS {nameof(CustomerProductPriceResponse.ProductSku)},
                 p.unit_of_measure_name AS {nameof(CustomerProductPriceResponse.UnitOfMeasureName)},
                 ct.id AS {nameof(CustomerProductPriceResponse.CategoryId)},
                 ct.name AS {nameof(CustomerProductPriceResponse.CategoryName)},
                 b.id AS {nameof(CustomerProductPriceResponse.BrandId)},
                 b.name AS {nameof(CustomerProductPriceResponse.BrandName)},
                 pg.id AS {nameof(CustomerProductPriceResponse.ProductGroupId)},
                 pg.name AS {nameof(CustomerProductPriceResponse.ProductGroupName)},
                 CAST(
                     CASE
                         -- When customer-specific pricing is available
                         WHEN cp.fixed_price IS NOT NULL 
                              AND cp.effective_date <= CAST('{DateOnly.FromDateTime(dateTimeProvider.UtcNow)}' AS DATE)
                              AND (cp.expiry_date > CAST('{DateOnly.FromDateTime(dateTimeProvider.UtcNow)}' AS DATE) OR cp.expiry_date IS NULL)
                              THEN cp.fixed_price
                         WHEN cp.fixed_discount IS NOT NULL 
                              AND cp.effective_date <= CAST('{DateOnly.FromDateTime(dateTimeProvider.UtcNow)}' AS DATE)
                              AND (cp.expiry_date > CAST('{DateOnly.FromDateTime(dateTimeProvider.UtcNow)}' AS DATE) OR cp.expiry_date IS NULL)
                              THEN pp.sale_price * (1 - cp.fixed_discount / 100.0)
                         -- Fallback to price-tier price
                         ELSE pp.sale_price
                     END AS DECIMAL(10, 2)
                 ) AS {nameof(CustomerProductPriceResponse.UnitPrice)},
                 p.tax_rate_type AS {nameof(CustomerProductPriceResponse.TaxRateType)},
                 CASE
                    WHEN cpb.is_deleted = false THEN true
                        ELSE false
                END AS {nameof(CustomerProductPriceResponse.IsBlacklisted)} 
             FROM crm.products p
             LEFT JOIN crm.customer_products cp 
                 ON cp.product_id = p.id AND cp.customer_id = @CustomerId AND cp.is_active = true
             INNER JOIN crm.categories ct ON ct.id = p.category_id
             INNER JOIN crm.brands b ON b.id = p.brand_id
             INNER JOIN crm.product_groups pg ON pg.id = p.product_group_id
             INNER JOIN crm.customers c ON c.id = @CustomerId
             INNER JOIN crm.price_tiers pt ON c.price_tier_id = pt.id
             INNER JOIN crm.product_prices pp ON pp.price_tier_id = pt.id AND pp.product_id = p.id
             LEFT JOIN crm.customer_product_blacklist cpb ON c.id = cpb.customer_id AND p.id = cpb.product_id AND cpb.is_deleted = false
             WHERE c.id = @CustomerId 
               AND p.sku = @ProductSku;
             """;

        var parameters = new { request.CustomerId, request.ProductSku };
        CustomerProductPriceResponse? customerProductPrice = await connection.QuerySingleOrDefaultAsync<CustomerProductPriceResponse>(sql, parameters);

        if (customerProductPrice is null)
        {
            return Result.Failure<CustomerProductPriceResponse>(CustomerProductErrors.NotFoundBySku(request.ProductSku));
        }
        return Result.Success(customerProductPrice);
    }
}
