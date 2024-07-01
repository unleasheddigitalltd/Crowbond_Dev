using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Common.Application.Pagination;
using Crowbond.Modules.CRM.Application.Suppliers.GetSuppliers.Dto;
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
            "id" => "s.id",
            "suppliername" => "s.suppliername",
            "accountnumber" => "s.accountnumber",
            _ => "c.suppliername" // Default sorting
        };

        string sql = $@"
            WITH FilteredCustomers AS (
                SELECT
                    sc.id                    AS {nameof(Supplier.Id)},
                    s.accountnumber         AS {nameof(Supplier.AccountNumber)},
                    s.suppliername          AS {nameof(Supplier.SupplierName)},
                    s.addressline1          AS {nameof(Supplier.AddressLine1)},                    
                    s.addressline2          AS {nameof(Supplier.AddressLine2)},
                    ROW_NUMBER() OVER (ORDER BY {orderByClause} {sortOrder}) AS RowNum
                FROM crm.suppliers s
                WHERE
                    s.suppliername ILIKE '%' || @Search || '%'
                    OR s.addressline1 ILIKE '%' || @Search || '%'                    
            )
            SELECT 
                c.{nameof(Supplier.Id)},
                c.{nameof(Supplier.SupplierName)},
                c.{nameof(Supplier.AddressLine1)},
                c.{nameof(Supplier.AddressLine2)},
            FROM FilteredSuppliers s
            WHERE s.RowNum BETWEEN ((@Page) * @Size) + 1 AND (@Page + 1) * @Size
            ORDER BY s.RowNum;

            SELECT Count(*) 
                FROM crm.suppliers s
                WHERE
                    s.suppliername ILIKE '%' || @Search || '%'
                    OR s.addressline1 ILIKE '%' || @Search || '%'
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


