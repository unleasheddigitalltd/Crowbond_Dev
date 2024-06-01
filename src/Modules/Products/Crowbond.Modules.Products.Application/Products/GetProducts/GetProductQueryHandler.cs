using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.Products.Application.Products.GetProducts.Dto;
using Dapper;

namespace Crowbond.Modules.Products.Application.Products.GetProducts;

internal sealed class GetProductQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetProductQuery, ProductsResponse>
{
    public async Task<Result<ProductsResponse>> Handle(
        GetProductQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id                     AS {nameof(Product.Id)},
                 sku                    AS {nameof(Product.Sku)},
                 name                   AS {nameof(Product.Name)},
                 filter_type_name       AS {nameof(Product.FilterTypeName)},
                 unit_of_measure_name   AS {nameof(Product.UnitOfMeasureName)},
                 active                 AS {nameof(Product.Active)}
             FROM products.products
             """;
        List<Product> products = (await connection.QueryAsync<Product>(sql, request)).AsList();
        return new ProductsResponse(products, new Pagination(15, 10, 1, 2, 0, 9));
    }
}

