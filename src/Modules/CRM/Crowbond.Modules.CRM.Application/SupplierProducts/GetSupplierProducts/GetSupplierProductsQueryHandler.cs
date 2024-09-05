using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.CRM.Application.SupplierProducts.GetSupplierProducts;

internal sealed class GetSupplierProductsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetSupplierProductsQuery, IReadOnlyCollection<SupplierProductResponse>>
{
    public async Task<Result<IReadOnlyCollection<SupplierProductResponse>>> Handle(GetSupplierProductsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(SupplierProductResponse.Id)},
                 supplier_id AS {nameof(SupplierProductResponse.SupplierId)},
                 product_id AS {nameof(SupplierProductResponse.ProductId)},
                 product_name AS {nameof(SupplierProductResponse.ProductName)},
                 product_sku AS {nameof(SupplierProductResponse.ProductSku)},
                 unit_of_measure_name AS {nameof(SupplierProductResponse.UnitOfMeasureName)},
                 category_id AS {nameof(SupplierProductResponse.CategoryId)},
                 unit_price AS {nameof(SupplierProductResponse.UnitPrice)},
                 tax_rate_type AS {nameof(SupplierProductResponse.TaxRateType)},
                 is_default AS {nameof(SupplierProductResponse.IsDefault)},
                 comments AS {nameof(SupplierProductResponse.Comments)}
             FROM crm.supplier_products
             WHERE supplier_id = @SupplierId             
             ORDER BY is_primary DESC, is_active DESC;
             """;

        List<SupplierProductResponse> supplierProducts = (await connection.QueryAsync<SupplierProductResponse>(sql, request)).AsList();

        return supplierProducts;
    }
}
