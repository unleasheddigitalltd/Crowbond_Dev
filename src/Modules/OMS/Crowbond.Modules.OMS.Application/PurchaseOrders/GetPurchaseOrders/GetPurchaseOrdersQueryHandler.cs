using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Common.Application.Pagination;
using Dapper;
using Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrder;
using Crowbond.Modules.OMS.Application.PurchaseOrders.CreatePurchaseOrder;
using Crowbond.Modules.OMS.Domain.PurchaseOrders;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrders;

internal sealed class GetPurchaseOrdersQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPurchaseOrdersQuery, PurchaseOrdersResponse>
{
    public async Task<Result<PurchaseOrdersResponse>> Handle(
        GetPurchaseOrdersQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string sortOrder = request.Order.Equals("ASC", StringComparison.OrdinalIgnoreCase) ? "ASC" : "DESC";
        string orderByClause = request.Sort switch
        {
            "id" => "s.id",
            "suppliername" => "po.supplier_name",
            "purchaseordernumber" => "p.purchase_order_number",
            _ => "po.supplier_name" // Default sorting
        };

        string sql = $@"
            WITH FilteredPurchaseOrders AS (
                SELECT
                    sc.id                    AS {nameof(PurchaseOrder.Id)},
                    s.account_number         AS {nameof(PurchaseOrder.PurchaseOrderNo)},
                    s.supplier_name          AS {nameof(PurchaseOrder.SupplierName)},
                    s.address_line1          AS {nameof(PurchaseOrder.AddressLine1)},                    
                    s.address_line2          AS {nameof(PurchaseOrder.AddressLine2)},
                    s.supplier_contact       AS {nameof(PurchaseOrder.SupplierContact)},
                    s.supplier_email          AS {nameof(PurchaseOrder.SupplierEmail)},
                    s.supplier_phone          AS {nameof(PurchaseOrder.SupplierPhone)},
                    ROW_NUMBER() OVER (ORDER BY {orderByClause} {sortOrder}) AS RowNum
                FROM oms.purchase_order_headers po
                WHERE
                    po.supplier_name ILIKE '%' || @Search || '%'
                    OR po.address_line1 ILIKE '%' || @Search || '%'                    
            )
            SELECT 
                s.{nameof(PurchaseOrderHeader.Id)},
                s.{nameof(PurchaseOrderHeader.PurchaseOrderNo)},
                s.{nameof(PurchaseOrderHeader.SupplierName)},
                s.{nameof(PurchaseOrderHeader.LocationName)},
                s.{nameof(PurchaseOrderHeader.ShippingAddressLine1)}
            FROM FilteredPurchaseOrders s
            WHERE .RowNum BETWEEN ((@Page) * @Size) + 1 AND (@Page + 1) * @Size
            ORDER BY s.RowNum;

            SELECT Count(*) 
                FROM crm.suppliers s
                WHERE
                    s.supplier_name ILIKE '%' || @Search || '%'
                    OR s.address_line1 ILIKE '%' || @Search || '%'
        ";

        SqlMapper.GridReader multi = await connection.QueryMultipleAsync(sql, request);

        var purchaseOrders = (await multi.ReadAsync<PurchaseOrder>()).ToList();
        int totalCount = await multi.ReadSingleAsync<int>();

        int totalPages = (int)Math.Ceiling(totalCount / (double)request.Size);
        int currentPage = request.Page;
        int pageSize = request.Size;
        int startIndex = (currentPage - 1) * pageSize;
        int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);

        return new PurchaseOrdersResponse(purchaseOrders, new Pagination(totalCount, pageSize, currentPage, totalPages, startIndex, endIndex));
    }
}


