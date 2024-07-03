using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Common.Application.Pagination;
using Dapper;

namespace Crowbond.Modules.CRM.Application.Customers.GetCustomers;

internal sealed class GetCustomersQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetCustomersQuery, CustomersResponse>
{
    public async Task<Result<CustomersResponse>> Handle(
        GetCustomersQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string sortOrder = request.Order.Equals("ASC", StringComparison.OrdinalIgnoreCase) ? "ASC" : "DESC";
        string orderByClause = request.Sort switch
        {
            "businessName" => "c.business_name",
            "accountNumber" => "c.account_number",
            "CustomerContact" => "c.customer_contact",
            "active" => "p.active",
            _ => "c.business_name" // Default sorting
        };

        string sql = $@"
            WITH FilteredCustomers AS (
                SELECT
                    c.id AS {nameof(Customer.Id)},
                    c.account_number AS {nameof(Customer.AccountNumber)}, 
                    c.business_name AS {nameof(Customer.BusinessName)},
                    c.customer_contact AS {nameof(Customer.CustomerContact)},
                    c.shipping_address_line1 AS {nameof(Customer.ShippingAddressLine1)},
                    c.shipping_address_line2 AS {nameof(Customer.ShippingAddressLine2)},
                    c.customer_phone AS {nameof(Customer.CustomerPhone)},
                    ROW_NUMBER() OVER (ORDER BY {orderByClause} {sortOrder}) AS RowNum
                FROM crm.customers c
                WHERE
                    c.business_name ILIKE '%' || @Search || '%'
                    OR c.account_number ILIKE '%' || @Search || '%'
                    OR c.customer_contact ILIKE '%' || @Search || '%'
                    OR c.shipping_address_line1 ILIKE '%' || @Search || '%'
                    OR c.shipping_address_line2 ILIKE '%' || @Search || '%'
                    OR c.customer_phone ILIKE '%' || @Search || '%'                    
            )
            SELECT 
                c.{nameof(Customer.Id)},
                c.{nameof(Customer.AccountNumber)},
                c.{nameof(Customer.BusinessName)},
                c.{nameof(Customer.CustomerContact)},
                c.{nameof(Customer.ShippingAddressLine1)},
                c.{nameof(Customer.ShippingAddressLine2)},
                c.{nameof(Customer.CustomerPhone)}
            FROM FilteredCustomers c
            WHERE c.RowNum BETWEEN ((@Page) * @Size) + 1 AND (@Page + 1) * @Size
            ORDER BY c.RowNum;

            SELECT Count(*) 
                FROM crm.customers c
                WHERE
                    c.business_name ILIKE '%' || @Search || '%'
                    OR c.account_number ILIKE '%' || @Search || '%'
                    OR c.customer_contact ILIKE '%' || @Search || '%'
                    OR c.shipping_address_line1 ILIKE '%' || @Search || '%'
                    OR c.shipping_address_line2 ILIKE '%' || @Search || '%'
                    OR c.customer_phone ILIKE '%' || @Search || '%'  
        ";

        SqlMapper.GridReader multi = await connection.QueryMultipleAsync(sql, request);

        var customers = (await multi.ReadAsync<Customer>()).ToList();
        int totalCount = await multi.ReadSingleAsync<int>();

        int totalPages = (int)Math.Ceiling(totalCount / (double)request.Size);
        int currentPage = request.Page;
        int pageSize = request.Size;
        int startIndex = (currentPage - 1) * pageSize;
        int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);

        return new CustomersResponse(customers, new Pagination(totalCount, pageSize, currentPage, totalPages, startIndex, endIndex));
    }
}


