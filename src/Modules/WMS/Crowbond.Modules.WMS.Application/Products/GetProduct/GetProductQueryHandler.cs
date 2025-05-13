using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Products;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Products.GetProduct;

internal sealed class GetProductQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetProductQuery, ProductResponse>
{
    public async Task<Result<ProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT 
                 p.id AS {nameof(ProductResponse.Id)},
                 p.sku AS {nameof(ProductResponse.Sku)},
                 p.name AS {nameof(ProductResponse.Name)},
                 p.unit_of_measure_name AS {nameof(ProductResponse.UnitOfMeasureName)},
                 c.name AS {nameof(ProductResponse.CategoryName)},           
                 p.default_location AS {nameof(ProductResponse.DefaultLocation)},  
                 COALESCE(SUM(s.current_qty), 0) AS {nameof(ProductResponse.Stock)},
                 p.reorder_level AS {nameof(ProductResponse.ReorderLevel)},
                 p.is_active AS {nameof(ProductResponse.IsActive)}
             FROM wms.products p
             INNER JOIN wms.categories c ON c.id = p.category_id
             LEFT OUTER JOIN wms.stocks s ON p.id = s.product_id             
             WHERE p.id = @ProductId
             GROUP BY 
                 p.id,
                 p.sku,
                 p.name,
                 p.default_location,
                 p.unit_of_measure_name,
                 c.name, 
                 p.reorder_level,
                 p.is_active
             """;

        ProductResponse? product = await connection.QuerySingleOrDefaultAsync<ProductResponse>(sql, request);

        if (product is null)
        {
            return Result.Failure<ProductResponse>(ProductErrors.NotFound(request.ProductId));
        }

        return product;
    }
}
