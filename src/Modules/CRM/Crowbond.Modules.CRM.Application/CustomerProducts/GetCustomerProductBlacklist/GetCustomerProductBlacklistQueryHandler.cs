using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Domain.CustomerProducts;
using Dapper;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProductBlacklist;

internal sealed class GetCustomerProductBlacklistQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetCustomerProductBlacklistQuery, ProductResponse>
{
    public async Task<Result<ProductResponse>> Handle(GetCustomerProductBlacklistQuery request, CancellationToken cancellationToken)
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
                 cp.comments AS {nameof(ProductResponse.Comments)}
             FROM crm.customer_product_blacklist cp             
             INNER JOIN crm.products p ON cp.product_id = p.id
             INNER JOIN crm.categories c ON p.category_id = c.id
             INNER JOIN crm.brands b ON p.brand_id = b.id
             INNER JOIN crm.product_groups pg ON p.product_group_id = pg.id
             WHERE cp.customer_id = @CustomerId AND p.id = @ProductId AND is_deleted = false
             """;

        ProductResponse? customerProduct = await connection.QuerySingleOrDefaultAsync<ProductResponse>(sql, request);

        if (customerProduct is null)
        {
            return Result.Failure<ProductResponse>(CustomerProductErrors.BlacklistNotFound(request.ProductId));
        }

        return customerProduct;
    }
}
