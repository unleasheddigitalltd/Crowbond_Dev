using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Application.Pagination;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetExpiredCustomerProducts;

internal sealed class GetExpiredCustomerProductsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetExpiredCustomerProductsQuery, CustomerProductsResponse>
{
    public async Task<Result<CustomerProductsResponse>> Handle(GetExpiredCustomerProductsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string sortOrder = request.Order.Equals("ASC", StringComparison.OrdinalIgnoreCase) ? "ASC" : "DESC";
        string orderByClause = request.Sort switch
        {
            "BusinessName" => "c.business_name",
            "ProductName" => "p.name",
            _ => "c.business_name" // Default sorting
        };
        string sql =
            $@"
            WITH FilteredCustomerProducts AS (
                SELECT
                    cp.id AS {nameof(CustomerProduct.Id)},
                    cp.customer_id AS {nameof(CustomerProduct.CustomerId)},
                    c.business_name AS {nameof(CustomerProduct.BusinessNema)},
                    p.id AS {nameof(CustomerProduct.ProductId)},
                    p.name AS {nameof(CustomerProduct.ProductName)},
                    pp.base_purchase_price AS {nameof(CustomerProduct.BasePurchasePrice)},
                    pp.sale_price AS {nameof(CustomerProduct.SalePrice)},
                    cp.fixed_price AS {nameof(CustomerProduct.FixedPrice)},
                    cp.fixed_discount AS {nameof(CustomerProduct.FixedDiscount)},
                    cp.effective_date AS {nameof(CustomerProduct.EffectiveDate)},
                    cp.expiry_date AS {nameof(CustomerProduct.ExpiryDate)},
                    ROW_NUMBER() OVER (ORDER BY {orderByClause} {sortOrder}) AS RowNum
                FROM crm.customer_products cp
                INNER JOIN crm.customers c ON c.id = cp.customer_id
                INNER JOIN crm.products p ON cp.product_id = p.id
                INNER JOIN crm.price_tiers pt ON c.price_tier_id = pt.id
                INNER JOIN crm.product_prices pp ON pt.id = pp.price_tier_id AND pp.product_id = p.id 
                WHERE cp.is_active = true
                   AND cp.expiry_date <= CAST(@ExpiryDate AS DATE)
                   AND (cp.fixed_price IS NOT NULL OR cp.fixed_discount IS NOT NULL) 
                   AND (c.business_name ILIKE '%' || @Search || '%'
                       OR p.name ILIKE '%' || @Search || '%')
            )
            SELECT 
                cp.{nameof(CustomerProduct.Id)},
                cp.{nameof(CustomerProduct.CustomerId)},
                cp.{nameof(CustomerProduct.BusinessNema)},
                cp.{nameof(CustomerProduct.ProductId)},
                cp.{nameof(CustomerProduct.ProductName)},
                cp.{nameof(CustomerProduct.BasePurchasePrice)},
                cp.{nameof(CustomerProduct.SalePrice)},
                cp.{nameof(CustomerProduct.FixedPrice)},
                cp.{nameof(CustomerProduct.FixedDiscount)},
                cp.{nameof(CustomerProduct.EffectiveDate)},
                cp.{nameof(CustomerProduct.ExpiryDate)}
            FROM FilteredCustomerProducts cp
            WHERE cp.RowNum BETWEEN ((@Page) * @Size) + 1 AND (@Page + 1) * @Size
            ORDER BY cp.RowNum;

            SELECT Count(*)
            FROM crm.customer_products cp
                INNER JOIN crm.customers c ON c.id = cp.customer_id
                INNER JOIN crm.products p ON cp.product_id = p.id
                INNER JOIN crm.price_tiers pt ON c.price_tier_id = pt.id
                INNER JOIN crm.product_prices pp ON pt.id = pp.price_tier_id AND pp.product_id = p.id 
                WHERE cp.is_active = true
                   AND cp.expiry_date <= CAST(@ExpiryDate AS DATE)
                   AND (cp.fixed_price IS NOT NULL OR cp.fixed_discount IS NOT NULL) 
                   AND (c.business_name ILIKE '%' || @Search || '%'
                       OR p.name ILIKE '%' || @Search || '%')
             ";

        SqlMapper.GridReader multi = await connection.QueryMultipleAsync(sql, request);

        var customerProducts = (await multi.ReadAsync<CustomerProduct>()).ToList();
        int totalCount = await multi.ReadSingleAsync<int>();

        int totalPages = (int)Math.Ceiling(totalCount / (double)request.Size);
        int currentPage = request.Page;
        int pageSize = request.Size;
        int startIndex = currentPage * pageSize;
        int endIndex = totalCount == 0 ? 0 : Math.Min(startIndex + pageSize - 1, totalCount - 1);

        return new CustomerProductsResponse(customerProducts, new Pagination(totalCount, pageSize, currentPage, totalPages, startIndex, endIndex));
    }
}
