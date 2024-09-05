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
             WHERE supplier_id = @SupplierId AND product_id = @ProductId
             """;

        SupplierProductResponse? supplierProduct = await connection.QuerySingleOrDefaultAsync<SupplierProductResponse>(sql, request);

        if (supplierProduct is null)
        {
            return Result.Failure<SupplierProductResponse>(SupplierProductErrors.NotFound(request.SupplierId, request.ProductId));
        }
        return supplierProduct;
    }
}
