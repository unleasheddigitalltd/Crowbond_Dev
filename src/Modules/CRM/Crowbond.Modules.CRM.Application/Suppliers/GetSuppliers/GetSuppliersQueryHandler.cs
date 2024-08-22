using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Common.Application.Pagination;
using Dapper;

namespace Crowbond.Modules.CRM.Application.Suppliers.GetSuppliers;

internal sealed class GetSuppliersQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetSuppliersQuery, SuppliersResponse>
{
    public async Task<Result<SuppliersResponse>> Handle(
        GetSuppliersQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string sortOrder = request.Order.Equals("ASC", StringComparison.OrdinalIgnoreCase) ? "ASC" : "DESC";
        string orderByClause = request.Sort switch
        {
            "supplierName" => nameof(Supplier.SupplierName),
            "accountNumber" => nameof(Supplier.AccountNumber),
            "isActive" => nameof(Supplier.IsActive),
            _ => nameof(Supplier.AccountNumber) // Default sorting
        };

        string sql = $@"
            WITH FilteredSuppliers AS (
                SELECT
                    s.id AS {nameof(Supplier.Id)},
                    s.account_number AS {nameof(Supplier.AccountNumber)},
                    s.supplier_name AS {nameof(Supplier.SupplierName)},
                    s.address_line1 AS {nameof(Supplier.AddressLine1)},                    
                    s.address_line2 AS {nameof(Supplier.AddressLine2)},
                    s.town_city AS {nameof(Supplier.TownCity)},
                    s.county AS {nameof(Supplier.County)},
                    s.country AS {nameof(Supplier.Country)},
                    s.postal_code AS {nameof(Supplier.PostalCode)},
                    s.is_active AS {nameof(Supplier.IsActive)},
                    t.first_name AS {nameof(Supplier.FirstName)},
                    t.last_name AS {nameof(Supplier.LastName)},
                    t.phone_number AS {nameof(Supplier.PhoneNumber)},
                    t.email AS {nameof(Supplier.Email)},
                    ROW_NUMBER() OVER (PARTITION BY s.id ORDER BY t.primary DESC) AS FilterRowNum
                FROM crm.suppliers s
                LEFT JOIN crm.supplier_contacts t ON s.id = t.supplier_id
                WHERE
                    s.supplier_name ILIKE '%' || @Search || '%'
                    OR s.account_number ILIKE '%' || @Search || '%'
                    OR s.address_line1 ILIKE '%' || @Search || '%'
                    OR s.address_line2 ILIKE '%' || @Search || '%'       
                    OR s.town_city ILIKE '%' || @Search || '%'    
                    OR s.county ILIKE '%' || @Search || '%'
                    OR s.country ILIKE '%' || @Search || '%'
                    OR t.first_name ILIKE '%' || @Search || '%' 
                    OR t.last_name ILIKE '%' || @Search || '%'
            ),
            RankedSuppliers AS (
                SELECT
                    *,
                    ROW_NUMBER() OVER (ORDER BY {orderByClause} {sortOrder}) AS RowNum
                FROM FilteredSuppliers
                WHERE FilterRowNum = 1
            )
            SELECT 
                s.{nameof(Supplier.Id)},
                s.{nameof(Supplier.AccountNumber)},
                s.{nameof(Supplier.SupplierName)},
                s.{nameof(Supplier.AddressLine1)},
                s.{nameof(Supplier.AddressLine2)},
                s.{nameof(Supplier.TownCity)},
                s.{nameof(Supplier.County)},
                s.{nameof(Supplier.Country)},
                s.{nameof(Supplier.PostalCode)},
                s.{nameof(Supplier.FirstName)},
                s.{nameof(Supplier.LastName)},
                s.{nameof(Supplier.PhoneNumber)},
                s.{nameof(Supplier.Email)}
            FROM RankedSuppliers s
            WHERE s.RowNum BETWEEN ((@Page) * @Size) + 1 AND (@Page + 1) * @Size
            ORDER BY s.RowNum;

            SELECT Count(*) 
            FROM (
                SELECT
                    s.id
                FROM crm.suppliers s
                LEFT JOIN crm.supplier_contacts t ON s.id = t.supplier_id
                WHERE
                    s.supplier_name ILIKE '%' || @Search || '%'
                    OR s.account_number ILIKE '%' || @Search || '%'
                    OR s.address_line1 ILIKE '%' || @Search || '%'
                    OR s.address_line2 ILIKE '%' || @Search || '%'       
                    OR s.town_city ILIKE '%' || @Search || '%'    
                    OR s.county ILIKE '%' || @Search || '%'
                    OR s.country ILIKE '%' || @Search || '%'
                    OR t.first_name ILIKE '%' || @Search || '%' 
                    OR t.last_name ILIKE '%' || @Search || '%'
                GROUP BY s.id
            ) AS UniqueSuppliers
        ";

        SqlMapper.GridReader multi = await connection.QueryMultipleAsync(sql, request);

        var suppliers = (await multi.ReadAsync<Supplier>()).ToList();
        int totalCount = await multi.ReadSingleAsync<int>();

        int totalPages = (int)Math.Ceiling(totalCount / (double)request.Size);
        int currentPage = request.Page;
        int pageSize = request.Size;
        int startIndex = currentPage * pageSize;
        int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);

        return new SuppliersResponse(suppliers, new Pagination(totalCount, pageSize, currentPage, totalPages, startIndex, endIndex));
    }
}


