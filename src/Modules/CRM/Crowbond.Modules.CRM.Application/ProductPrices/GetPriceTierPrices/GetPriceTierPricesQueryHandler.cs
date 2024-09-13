using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.CRM.Application.ProductPrices.GetPriceTierPrices;

internal sealed class GetPriceTierPricesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPriceTierPricesQuery, IReadOnlyCollection<ProductPriceResponse>>
{
    public async Task<Result<IReadOnlyCollection<ProductPriceResponse>>> Handle(GetPriceTierPricesQuery request, CancellationToken cancellationToken)
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
                 p.category_id AS {nameof(ProductPriceResponse.CategoryId)},
                 p.category_name AS {nameof(ProductPriceResponse.CategoryName)},
                 pr.price_tier_id AS {nameof(ProductPriceResponse.PriceTierId)},
                 pr.base_purchase_price AS {nameof(ProductPriceResponse.BasePurchasePrice)},
                 pr.sale_price AS {nameof(ProductPriceResponse.SalePrice)},
                 pr.effective_date AS {nameof(ProductPriceResponse.EffectiveDate)}              
             FROM crm.product_prices pr
             INNER JOIN crm.products p ON p.id = pr.product_id
             WHERE pr.price_tier_id = @PriceTierId AND pr.is_deleted = false
             """;

        List<ProductPriceResponse> productPrices = (await connection.QueryAsync<ProductPriceResponse>(sql, request)).AsList();

        return productPrices;
    }
}
