using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Customers.GetCustomers.Dto;
using Dapper;
using Product = Crowbond.Modules.CRM.Application.Customers.GetCustomers.Dto.Customer;

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
            "id" => "c.id",
            "businessname" => "c.businessname",
            "accountnumber" => "c.accountnumber",
            "active" => "p.active",
            _ => "c.businessname" // Default sorting
        };

        string sql = $@"
            WITH FilteredCustomers AS (
                SELECT
                    c.id                            AS {nameof(Customer.Id)},
                    c.businessname                  AS {nameof(Customer.BusinessName)},
                    c.shippingaddressline1          AS {nameof(Customer.ShippingAddressLine1)},                    
                    c.shippingaddressline2          AS {nameof(Customer.ShippingAddressLine2)},
                    ROW_NUMBER() OVER (ORDER BY {orderByClause} {sortOrder}) AS RowNum
                FROM crm.customers c
                WHERE
                    c.businessname ILIKE '%' || @Search || '%'
                    OR c.shippingaddressline1 ILIKE '%' || @Search || '%'                    
            )
            SELECT 
                c.{nameof(Customer.Id)},
                c.{nameof(Customer.BusinessName)},
                c.{nameof(Customer.ShippingAddressLine1)},
                c.{nameof(Customer.ShippingAddressLine2)},
            FROM FilteredCustomers c
            WHERE c.RowNum BETWEEN ((@Page) * @Size) + 1 AND (@Page + 1) * @Size
            ORDER BY p.RowNum;

            SELECT Count(*) 
                FROM crm.customers c
                WHERE
                    c.businessname ILIKE '%' || @Search || '%'
                    OR c.shippingaddressline1 ILIKE '%' || @Search || '%'
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


