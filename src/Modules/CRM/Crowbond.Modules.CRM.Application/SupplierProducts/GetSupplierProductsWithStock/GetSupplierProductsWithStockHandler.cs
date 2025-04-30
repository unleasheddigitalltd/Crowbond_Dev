using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.CRM.Application.SupplierProducts.GetSupplierProductsWithStock;

internal sealed class GetSupplierProductsWithStockHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetSupplierProductsWithStockQuery, IReadOnlyCollection<ProductWithStockResponse>>
{
    public async Task<Result<IReadOnlyCollection<ProductWithStockResponse>>> Handle(GetSupplierProductsWithStockQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        
        const string sql = """
                           SELECT
                               p.id                     AS Id,
                               p.name                   AS ProductName,
                               p.sku                    AS ProductSku,
                               p.unit_of_measure_name   AS UnitOfMeasureName,
                               c.name                   AS CategoryName,
                               b.name                   AS BrandName,
                               pg.name                  AS ProductGroupName,
                               sp.unit_price            AS UnitPrice,
                               p.tax_rate_type          AS TaxRateType,
                               COALESCE(SUM(s.current_qty), 0) AS StockLevel
                           FROM crm.supplier_products sp
                           INNER JOIN crm.products p 
                               ON sp.product_id = p.id
                           INNER JOIN crm.categories c 
                               ON p.category_id = c.id
                           INNER JOIN crm.brands b 
                               ON p.brand_id = b.id
                           INNER JOIN crm.product_groups pg 
                               ON p.product_group_id = pg.id
                           LEFT JOIN wms.stocks s 
                               ON p.id = s.product_id
                           WHERE
                               sp.supplier_id = @SupplierId
                               AND p.is_active   = TRUE
                               AND sp.is_deleted = FALSE
                           GROUP BY
                               p.id,
                               p.name,
                               p.sku,
                               p.unit_of_measure_name,
                               c.name,
                               b.name,
                               pg.name,
                               sp.unit_price,
                               p.tax_rate_type
                           """;

        var list = (await connection
                .QueryAsync<ProductWithStockResponse>(
                    sql,
                    new { request.SupplierId }
                ))
            .AsList();

        return list;

    }
}
