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
            "id" => "id",
            "supplierName" => "supplier_name",
            "accountNumber" => "account_number",
            _ => "s.supplier_name" // Default sorting
        };

        string sql = $@"
            WITH FilteredSuppliers AS (
                SELECT
                    id AS {nameof(Supplier.Id)},
                    account_number AS {nameof(Supplier.AccountNumber)},
                    supplier_name AS {nameof(Supplier.SupplierName)},
                    address_line1 AS {nameof(Supplier.AddressLine1)},                    
                    address_line2 AS {nameof(Supplier.AddressLine2)},
                    town_city AS {nameof(Supplier.TownCity)},
                    county AS {nameof(Supplier.County)},
                    country AS {nameof(Supplier.Country)},
                    postal_code AS {nameof(Supplier.PostalCode)},
                    ROW_NUMBER() OVER (ORDER BY {orderByClause} {sortOrder}) AS RowNum
                FROM crm.suppliers s
                WHERE
                    supplier_name ILIKE '%' || @Search || '%'
                    OR account_number ILIKE '%' || @Search || '%'
                    OR address_line1 ILIKE '%' || @Search || '%'
                    OR address_line2 ILIKE '%' || @Search || '%'       
                    OR town_city ILIKE '%' || @Search || '%'                    
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
                s.{nameof(Supplier.PostalCode)}
            FROM FilteredSuppliers s
            WHERE s.RowNum BETWEEN ((@Page) * @Size) + 1 AND (@Page + 1) * @Size
            ORDER BY s.RowNum;

            SELECT Count(*) 
                FROM crm.suppliers s
                WHERE
                    supplier_name ILIKE '%' || @Search || '%'
                    OR account_number ILIKE '%' || @Search || '%'
                    OR address_line1 ILIKE '%' || @Search || '%'
                    OR address_line2 ILIKE '%' || @Search || '%'       
                    OR town_city ILIKE '%' || @Search || '%'
        ";

        SqlMapper.GridReader multi = await connection.QueryMultipleAsync(sql, request);

        var suppliers = (await multi.ReadAsync<Supplier>()).ToList();
        int totalCount = await multi.ReadSingleAsync<int>();

        int totalPages = (int)Math.Ceiling(totalCount / (double)request.Size);
        int currentPage = request.Page;
        int pageSize = request.Size;
        int startIndex = (currentPage - 1) * pageSize;
        int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);

        return new SuppliersResponse(suppliers, new Pagination(totalCount, pageSize, currentPage, totalPages, startIndex, endIndex));
    }
}


