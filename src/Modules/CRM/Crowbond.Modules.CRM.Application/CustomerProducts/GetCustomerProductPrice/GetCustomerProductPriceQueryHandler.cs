using System.Data.Common;
using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Domain.CustomerProducts;
using Dapper;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProductPrice;

internal sealed class GetCustomerProductPriceQueryHandler(
    IDbConnectionFactory dbConnectionFactory,
    IDateTimeProvider dateTimeProvider)
    : IQueryHandler<GetCustomerProductPriceQuery, CustomerProductPriceResponse>
{
    public async Task<Result<CustomerProductPriceResponse>> Handle(GetCustomerProductPriceQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string sql =
            $"""
             SELECT
                 cp.id AS {nameof(CustomerProductPriceResponse.Id)},
                 cp.customer_id AS {nameof(CustomerProductPriceResponse.CustomerId)},
                 cp.product_id AS {nameof(CustomerProductPriceResponse.ProductId)},
                 p.name AS {nameof(CustomerProductPriceResponse.ProductName)},
                 p.sku AS {nameof(CustomerProductPriceResponse.ProductSku)},
                 p.unit_of_measure_name AS {nameof(CustomerProductPriceResponse.UnitOfMeasureName)},
                 c.id AS {nameof(CustomerProductPriceResponse.CategoryId)},
                 ct.name AS {nameof(CustomerProductPriceResponse.CategoryName)},
                 b.id AS {nameof(CustomerProductPriceResponse.BrandId)},
                 b.name AS {nameof(CustomerProductPriceResponse.BrandName)},
                 pg.id AS {nameof(CustomerProductPriceResponse.ProductGroupId)},
                 pg.name AS {nameof(CustomerProductPriceResponse.ProductGroupName)},
                 CAST(CASE
             	 	WHEN c.no_discount_fixed_price = true AND
                    CASE 
                        WHEN (cp.fixed_price IS NOT NULL OR cp.fixed_discount IS NOT NULL)             
                            AND cp.effective_date <= CAST('{DateOnly.FromDateTime(dateTimeProvider.UtcNow)}' AS DATE)
                            AND cp.expiry_date > CAST('{DateOnly.FromDateTime(dateTimeProvider.UtcNow)}' AS DATE)
                            THEN TRUE
                        ELSE FALSE
                    END = true
             	 	THEN 1 
             	 	ELSE (1 - c.discount / 100)
             	 END * 
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
                    END AS DECIMAL(10, 2)) AS  {nameof(CustomerProductPriceResponse.UnitPrice)},
                 p.tax_rate_type AS {nameof(CustomerProductPriceResponse.TaxRateType)}
             FROM crm.customer_products cp
             INNER JOIN crm.products p ON cp.product_id = p.id
             INNER JOIN crm.categories ct ON ct.id = p.category_id
             INNER JOIN crm.brands b ON b.id = p.brand_id
             INNER JOIN crm.product_groups pg ON pg.id = p.product_group_id
             INNER JOIN crm.customers c ON cp.customer_id = c.id
             INNER JOIN crm.price_tiers pt ON c.price_tier_id = pt.id
             INNER JOIN crm.product_prices pp ON pp.price_tier_id = pt.id AND pp.product_id = p.id
             WHERE c.id = @CustomerId 
               AND p.id = @ProductId 
               AND cp.is_active = true;
             """;

        CustomerProductPriceResponse? customerProductPrice = await connection.QuerySingleOrDefaultAsync<CustomerProductPriceResponse>(sql, request);

        if (customerProductPrice is null)
        {
            return Result.Failure<CustomerProductPriceResponse>(CustomerProductErrors.NotFound(request.ProductId));
        }
        return customerProductPrice;
    }
}
