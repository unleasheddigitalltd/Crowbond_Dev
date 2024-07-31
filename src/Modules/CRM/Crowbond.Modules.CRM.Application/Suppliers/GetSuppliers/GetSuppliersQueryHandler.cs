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
            "supplierName" => "s.supplier_name",
            "accountNumber" => "s.account_number",
            _ => "s.supplier_name" // Default sorting
        };

        string sql = $@"
            WITH FilteredSuppliers AS (
                SELECT
                    s.id                    AS {nameof(Supplier.Id)},
                    s.account_number         AS {nameof(Supplier.AccountNumber)},
                    s.supplier_name          AS {nameof(Supplier.SupplierName)},
                    s.address_line1          AS {nameof(Supplier.AddressLine1)},                    
                    s.address_line2          AS {nameof(Supplier.AddressLine2)},
                    ROW_NUMBER() OVER (ORDER BY {orderByClause} {sortOrder}) AS RowNum
                FROM crm.suppliers s
                WHERE
                    s.supplier_name ILIKE '%' || @Search || '%'
                    OR s.address_line1 ILIKE '%' || @Search || '%'                    
            )
            SELECT 
                s.{nameof(Supplier.Id)},
                s.{nameof(Supplier.SupplierName)},
                s.{nameof(Supplier.AddressLine1)},
                s.{nameof(Supplier.AddressLine2)}
            FROM FilteredSuppliers s
            WHERE s.RowNum BETWEEN ((@Page) * @Size) + 1 AND (@Page + 1) * @Size
            ORDER BY s.RowNum;

            SELECT Count(*) 
                FROM crm.suppliers s
                WHERE
                    s.supplier_name ILIKE '%' || @Search || '%'
                    OR s.address_line1 ILIKE '%' || @Search || '%'
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


