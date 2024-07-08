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
            "id" => "s.id",
            "suppliername" => "s.supplier_name",
            "accountnumber" => "s.account_number",
            _ => "c.supplier_name" // Default sorting
        };

        string sql = $@"
            WITH FilteredCustomers AS (
                SELECT
                    sc.id                    AS {nameof(Supplier.Id)},
                    s.account_number         AS {nameof(Supplier.AccountNumber)},
                    s.supplier_name          AS {nameof(Supplier.SupplierName)},
                    s.address_line1          AS {nameof(Supplier.AddressLine1)},                    
                    s.address_line2          AS {nameof(Supplier.AddressLine2)},
                    s.supplier_contact       AS {nameof(Supplier.SupplierContact)},
                    s.supplier_email          AS {nameof(Supplier.SupplierEmail)},
                    s.supplier_phone          AS {nameof(Supplier.SupplierPhone)},
                    ROW_NUMBER() OVER (ORDER BY {orderByClause} {sortOrder}) AS RowNum
                FROM crm.suppliers s
                WHERE
                    s.supplier_name ILIKE '%' || @Search || '%'
                    OR s.address_line1 ILIKE '%' || @Search || '%'                    
            )
            SELECT 
                s.{nameof(Supplier.Id)},
                s.{nameof(Supplier.AccountNumber)},
                s.{nameof(Supplier.SupplierName)},
                s.{nameof(Supplier.AddressLine1)},
                s.{nameof(Supplier.AddressLine2)},
                s.{nameof(Supplier.SupplierContact)},
                s.{nameof(Supplier.SupplierEmail)},
                s.{nameof(Supplier.SupplierPhone)}
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


