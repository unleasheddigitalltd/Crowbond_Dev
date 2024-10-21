using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Products.GetProductsForProductGroup;

internal sealed class GetProductsForProductGroupQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetProductsForProductGroupQuery, IReadOnlyCollection<ProductResponse>>
{
    public async Task<Result<IReadOnlyCollection<ProductResponse>>> Handle(GetProductsForProductGroupQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 p.id AS {nameof(ProductResponse.Id)},
                 p.sku AS {nameof(ProductResponse.Sku)},
                 p.name AS {nameof(ProductResponse.Name)},
                 b.name AS {nameof(ProductResponse.BrandName)}
             FROM wms.products p
             INNER JOIN wms.brands b ON b.id = p.brand_id 
             WHERE p.product_group_id = @ProductGroupId AND p.is_active = true
             ORDER BY name
             """;

        List<ProductResponse> products = (await connection.QueryAsync<ProductResponse>(sql, request)).AsList();

        return products;
    }
}
