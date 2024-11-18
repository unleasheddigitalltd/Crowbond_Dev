using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Domain.CustomerContacts;
using Crowbond.Modules.CRM.Domain.CustomerProducts;
using Dapper;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProduct;

internal sealed class GetCustomerProductPriceQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetCustomerProductQuery, ProductResponse>
{
    public async Task<Result<ProductResponse>> Handle(GetCustomerProductQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 cp.id AS {nameof(ProductResponse.Id)},
                 cp.customer_id AS {nameof(ProductResponse.CustomerId)},
                 cp.product_id AS {nameof(ProductResponse.ProductId)},
                 p.name AS {nameof(ProductResponse.ProductName)},
                 p.sku AS {nameof(ProductResponse.ProductSku)},
                 p.unit_of_measure_name AS {nameof(ProductResponse.UnitOfMeasureName)},
                 c.name AS {nameof(ProductResponse.CategoryName)},
                 b.name AS {nameof(ProductResponse.BrandName)},
                 pg.name AS {nameof(ProductResponse.ProductGroupName)},
                 cp.fixed_price AS {nameof(ProductResponse.FixedPrice)},
                 cp.fixed_discount AS {nameof(ProductResponse.FixedDiscount)},
                 cp.comments AS {nameof(ProductResponse.Comments)},
                 cp.effective_date AS {nameof(ProductResponse.EffectiveDate)},
                 cp.expiry_date AS {nameof(ProductResponse.ExpiryDate)}
             FROM crm.customer_products cp             
             INNER JOIN crm.products p ON cp.product_id = p.id
             INNER JOIN crm.categories c ON p.category_id = c.id
             INNER JOIN crm.brands b ON p.brand_id = b.id
             INNER JOIN crm.product_groups pg ON p.product_group_id = pg.id
             WHERE cp.customer_id = @CustomerId AND p.id = @ProductId AND cp.is_active = true
             """;

        ProductResponse? customerProduct = await connection.QuerySingleOrDefaultAsync<ProductResponse>(sql, request);

        if (customerProduct is null)
        {
            return Result.Failure<ProductResponse>(CustomerProductErrors.NotFound(request.ProductId));
        }

        return customerProduct;
    }
}
