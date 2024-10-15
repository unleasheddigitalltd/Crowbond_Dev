using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Domain.SupplierProducts;
using Dapper;

namespace Crowbond.Modules.CRM.Application.SupplierProducts.GetSupplierProduct;

internal sealed class GetSupplierProductQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetSupplierProductQuery, SupplierProductResponse>
{
    public async Task<Result<SupplierProductResponse>> Handle(GetSupplierProductQuery request, CancellationToken cancellationToken)
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
                 c.id AS {nameof(SupplierProductResponse.CategoryId)},
                 c.name AS {nameof(SupplierProductResponse.CategoryName)},
                 b.id AS {nameof(SupplierProductResponse.BrandId)},
                 b.name AS {nameof(SupplierProductResponse.BrandName)},
                 pg.id AS {nameof(SupplierProductResponse.ProductGroupId)},
                 pg.name AS {nameof(SupplierProductResponse.ProductGroupName)},
                 sp.unit_price AS {nameof(SupplierProductResponse.UnitPrice)},
                 p.tax_rate_type AS {nameof(SupplierProductResponse.TaxRateType)},
                 sp.is_default AS {nameof(SupplierProductResponse.IsDefault)},
                 sp.comments AS {nameof(SupplierProductResponse.Comments)}
             FROM crm.supplier_products sp
             INNER JOIN crm.products p ON p.id = sp.product_id             
             INNER JOIN crm.categories c ON c.id = p.category_id
             INNER JOIN crm.brands b ON b.id = p.brand_id
             INNER JOIN crm.product_groups pg ON pg.id = p.product_group_id
             WHERE sp.supplier_id = @SupplierId AND p.id = @ProductId AND sp.is_deleted = false
             """;

        SupplierProductResponse? supplierProduct = await connection.QuerySingleOrDefaultAsync<SupplierProductResponse>(sql, request);

        if (supplierProduct is null)
        {
            return Result.Failure<SupplierProductResponse>(SupplierProductErrors.NotFound(request.SupplierId, request.ProductId));
        }
        return supplierProduct;
    }
}
