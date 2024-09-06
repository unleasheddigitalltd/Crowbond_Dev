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
                 sp.id AS {nameof(SupplierProductResponse.Id)},
                 sp.supplier_id AS {nameof(SupplierProductResponse.SupplierId)},
                 sp.product_id AS {nameof(SupplierProductResponse.ProductId)},
                 p.name AS {nameof(SupplierProductResponse.ProductName)},
                 p.sku AS {nameof(SupplierProductResponse.ProductSku)},
                 p.unit_of_measure_name AS {nameof(SupplierProductResponse.UnitOfMeasureName)},
                 p.category_id AS {nameof(SupplierProductResponse.CategoryId)},
                 sp.unit_price AS {nameof(SupplierProductResponse.UnitPrice)},
                 p.tax_rate_type AS {nameof(SupplierProductResponse.TaxRateType)},
                 sp.is_default AS {nameof(SupplierProductResponse.IsDefault)},
                 sp.comments AS {nameof(SupplierProductResponse.Comments)}
             FROM crm.supplier_products sp
             INNER JOIN crm.products p ON sp.product_id = p.id
             WHERE sp.supplier_id = @SupplierId AND p.is_active = true AND sp.is_deleted = false
             """;

        List<SupplierProductResponse> supplierProducts = (await connection.QueryAsync<SupplierProductResponse>(sql, request)).AsList();

        return supplierProducts;
    }
}
