using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.CRM.Application.ProductPrices.GetProductPrices;

internal sealed class GetProductPricesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetProductPricesQuery, IReadOnlyCollection<ProductPriceResponse>>
{
    public async Task<Result<IReadOnlyCollection<ProductPriceResponse>>> Handle(GetProductPricesQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 pr.id AS {nameof(ProductPriceResponse.Id)},
                 pr.product_id AS {nameof(ProductPriceResponse.ProductId)},
                 p.name AS {nameof(ProductPriceResponse.ProductName)},
                 p.sku AS {nameof(ProductPriceResponse.ProductSku)},
                 p.unit_of_measure_name AS {nameof(ProductPriceResponse.UnitOfMeasureName)},
                 c.name AS {nameof(ProductPriceResponse.CategoryName)},
                 b.name AS {nameof(ProductPriceResponse.BrandName)},
                 pg.name AS {nameof(ProductPriceResponse.ProductGroupName)},
                 pr.price_tier_id AS {nameof(ProductPriceResponse.PriceTierId)},
                 pr.base_purchase_price AS {nameof(ProductPriceResponse.BasePurchasePrice)},
                 pr.sale_price AS {nameof(ProductPriceResponse.SalePrice)},
                 pr.effective_date AS {nameof(ProductPriceResponse.EffectiveDate)}              
             FROM crm.product_prices pr
             INNER JOIN crm.products p ON p.id = pr.product_id
             INNER JOIN crm.categories c ON p.category_id = c.id
             INNER JOIN crm.brands b ON p.brand_id = b.id
             INNER JOIN crm.product_groups pg ON p.product_group_id = pg.id
             WHERE pr.product_id = @ProductId AND pr.is_deleted = false
             """;

        List<ProductPriceResponse> productPrices = (await connection.QueryAsync<ProductPriceResponse>(sql, request)).AsList();

        return productPrices;
    }
}
