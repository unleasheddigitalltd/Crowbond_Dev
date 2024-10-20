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
            "businessName" => nameof(Customer.BusinessName),
            "accountNumber" => nameof(Customer.AccountNumber),
            "active" => nameof(Customer.IsActive),
            _ => nameof(Customer.AccountNumber) // Default sorting
        };

        string sql = $@"
            WITH FilteredCustomers AS (
                SELECT
                    c.id,
                    c.account_number AS {nameof(Customer.AccountNumber)}, 
                    c.business_name AS {nameof(Customer.BusinessName)},
                    c.billing_address_line1 AS {nameof(Customer.BillingAddressLine1)},
                    c.billing_address_line2 AS {nameof(Customer.BillingAddressLine2)},
                    c.billing_town_city AS {nameof(Customer.BillingTownCity)},
                    c.billing_county AS {nameof(Customer.BillingCounty)},
                    c.billing_country AS {nameof(Customer.BillingCountry)},
                    c.billing_postal_code {nameof(Customer.BillingPostalCode)},
                    t.first_name AS {nameof(Customer.FirstName)},
                    t.last_name AS {nameof(Customer.LastName)},
                    t.phone_number AS {nameof(Customer.PhoneNumber)},
                    t.email AS {nameof(Customer.Email)},
                    c.is_active AS {nameof(Customer.IsActive)},
                    ROW_NUMBER() OVER (PARTITION BY c.id ORDER BY t.is_primary DESC) AS FilterRowNum
                FROM crm.customers c
                LEFT JOIN crm.customer_contacts t ON c.id = t.customer_id
                WHERE                    
                    c.business_name ILIKE '%' || @Search || '%'
                    OR c.account_number ILIKE '%' || @Search || '%'
                    OR c.billing_address_line1 ILIKE '%' || @Search || '%'
                    OR c.billing_address_line2 ILIKE '%' || @Search || '%'       
                    OR c.billing_town_city ILIKE '%' || @Search || '%'
                    OR c.billing_county ILIKE '%' || @Search || '%'
                    OR c.billing_country ILIKE '%' || @Search || '%'
                    OR t.first_name ILIKE '%' || @Search || '%' 
                    OR t.last_name ILIKE '%' || @Search || '%' 
            ),
            RankedCustomers AS (
                SELECT
                    *,
                    ROW_NUMBER() OVER (ORDER BY {orderByClause} {sortOrder}) AS RowNum
                FROM FilteredCustomers
                WHERE FilterRowNum = 1
            )
            SELECT 
                c.{nameof(Customer.Id)},
                c.{nameof(Customer.AccountNumber)},
                c.{nameof(Customer.BusinessName)},
                c.{nameof(Customer.BillingAddressLine1)},
                c.{nameof(Customer.BillingAddressLine2)},
                c.{nameof(Customer.BillingTownCity)},
                c.{nameof(Customer.BillingCounty)},
                c.{nameof(Customer.BillingCountry)},
                c.{nameof(Customer.BillingPostalCode)},
                c.{nameof(Customer.FirstName)},
                c.{nameof(Customer.LastName)},
                c.{nameof(Customer.PhoneNumber)},
                c.{nameof(Customer.Email)},
                c.{nameof( Customer.IsActive)}
            FROM RankedCustomers c
            WHERE c.RowNum BETWEEN ((@Page) * @Size) + 1 AND (@Page + 1) * @Size
            ORDER BY c.RowNum;

            SELECT COUNT(*)
            FROM (
                SELECT
                    c.id
                FROM crm.customers c
                LEFT JOIN crm.customer_contacts t ON c.id = t.customer_id
                WHERE                    
                    c.business_name ILIKE '%' || @Search || '%'
                    OR c.account_number ILIKE '%' || @Search || '%'
                    OR c.billing_address_line1 ILIKE '%' || @Search || '%'
                    OR c.billing_address_line2 ILIKE '%' || @Search || '%'       
                    OR c.billing_town_city ILIKE '%' || @Search || '%'
                    OR c.billing_county ILIKE '%' || @Search || '%'
                    OR c.billing_country ILIKE '%' || @Search || '%'
                    OR t.first_name ILIKE '%' || @Search || '%' 
                    OR t.last_name ILIKE '%' || @Search || '%' 
                GROUP BY c.id
            ) AS UniqueCustomers;
        ";

        SqlMapper.GridReader multi = await connection.QueryMultipleAsync(sql, request);

        var customers = (await multi.ReadAsync<Customer>()).ToList();
        int totalCount = await multi.ReadSingleAsync<int>();

        int totalPages = (int)Math.Ceiling(totalCount / (double)request.Size);
        int currentPage = request.Page;
        int pageSize = request.Size;
        int startIndex = currentPage * pageSize;
        int endIndex = totalCount == 0 ? 0 : Math.Min(startIndex + pageSize - 1, totalCount - 1);

        return new CustomersResponse(customers, new Pagination(totalCount, pageSize, currentPage, totalPages, startIndex, endIndex));
    }
}


