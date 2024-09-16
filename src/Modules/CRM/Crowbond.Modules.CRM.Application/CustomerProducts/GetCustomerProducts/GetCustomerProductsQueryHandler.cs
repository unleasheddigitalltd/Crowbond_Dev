using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProducts;

internal sealed class GetCustomerProductsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetCustomerProductsQuery, IReadOnlyCollection<CustomerProductResponse>>
{
    public async Task<Result<IReadOnlyCollection<CustomerProductResponse>>> Handle(GetCustomerProductsQuery request, CancellationToken cancellationToken)
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
                 p.category_id AS {nameof(CustomerProductResponse.CategoryId)},
                 p.category_name AS {nameof(CustomerProductResponse.CategoryName)},
                 cp.fixed_price AS {nameof(CustomerProductResponse.FixedPrice)},
                 cp.fixed_discount AS {nameof(CustomerProductResponse.FixedDiscount)},
                 cp.comments AS {nameof(CustomerProductResponse.Comments)},
                 cp.effective_date AS {nameof(CustomerProductResponse.EffectiveDate)},
                 cp.expiry_date AS {nameof(CustomerProductResponse.ExpiryDate)}
             FROM crm.customer_products cp
             INNER JOIN crm.products p ON cp.product_id = p.id
             WHERE cp.customer_id = @CustomerId AND cp.is_active = true
             """;

        List<CustomerProductResponse> customerProducts = (await connection.QueryAsync<CustomerProductResponse>(sql, request)).AsList();

        return customerProducts;
    }
}
