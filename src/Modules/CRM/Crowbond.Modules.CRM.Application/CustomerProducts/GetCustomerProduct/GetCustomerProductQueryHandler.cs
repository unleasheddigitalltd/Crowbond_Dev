using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProduct;

internal sealed class GetCustomerProductPriceQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetCustomerProductQuery, CustomerProductResponse>
{
    public async Task<Result<CustomerProductResponse>> Handle(GetCustomerProductQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 cp.id AS {nameof(CustomerProductResponse.Id)},
                 cp.customer_id AS {nameof(CustomerProductResponse.CustomerId)},
                 cp.product_id AS {nameof(CustomerProductResponse.ProductId)},
                 p.name AS {nameof(CustomerProductResponse.ProductName)},
                 p.sku AS {nameof(CustomerProductResponse.ProductSku)},
                 p.unit_of_measure_name AS {nameof(CustomerProductResponse.UnitOfMeasureName)},
                 c.name AS {nameof(CustomerProductResponse.CategoryName)},
                 b.name AS {nameof(CustomerProductResponse.BrandName)},
                 pg.name AS {nameof(CustomerProductResponse.ProductGroupName)},
                 cp.fixed_price AS {nameof(CustomerProductResponse.FixedPrice)},
                 cp.fixed_discount AS {nameof(CustomerProductResponse.FixedDiscount)},
                 cp.comments AS {nameof(CustomerProductResponse.Comments)},
                 cp.effective_date AS {nameof(CustomerProductResponse.EffectiveDate)},
                 cp.expiry_date AS {nameof(CustomerProductResponse.ExpiryDate)}
             FROM crm.customer_products cp             
             INNER JOIN crm.categories c ON p.category_id = c.id
             INNER JOIN crm.brands b ON p.brand_id = b.id
             INNER JOIN crm.product_groups pg ON p.product_group_id = pg.id
             INNER JOIN crm.products p ON cp.product_id = p.id
             WHERE cp.customer_id = @CustomerId AND p.id = @ProductId AND cp.is_active = true
             """;

        CustomerProductResponse? customerProduct = await connection.QuerySingleOrDefaultAsync<CustomerProductResponse>(sql, request);

        return customerProduct;
    }
}
