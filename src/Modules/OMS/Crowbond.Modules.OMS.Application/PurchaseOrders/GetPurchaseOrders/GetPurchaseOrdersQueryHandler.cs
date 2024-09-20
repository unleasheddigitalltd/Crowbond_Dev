using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Common.Application.Pagination;
using Dapper;

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
            "purchasedate" => "purchase_date",
            "suppliername" => "supplier_name",
            "purchaseordernumber" => "purchase_order_number",
            _ => "supplier_name" // Default sorting
        };

        string sql = $@"
            WITH FilteredPurchaseOrders AS (
                SELECT
                    id AS {nameof(PurchaseOrder.Id)},          
                    purchase_order_no AS {nameof(PurchaseOrder.PurchaseOrderNo)},
                    purchase_date AS {nameof(PurchaseOrder.PurchaseDate)},
                    supplier_name AS {nameof(PurchaseOrder.SupplierName)},
                    contact_full_name AS {nameof(PurchaseOrder.ContactFullName)},
                    contact_phone AS {nameof(PurchaseOrder.ContactPhone)},
                    contact_email AS {nameof(PurchaseOrder.ContactEmail)},
                    shipping_location_name AS {nameof(PurchaseOrder.ShippingLocationName)},
                    shipping_address_line1 AS {nameof(PurchaseOrder.ShippingAddressLine1)},
                    shipping_address_line2 AS {nameof(PurchaseOrder.ShippingAddressLine2)},
                    shipping_town_city AS {nameof(PurchaseOrder.ShippingTownCity)},
                    shipping_county AS {nameof(PurchaseOrder.ShippingCounty)},
                    shipping_country AS {nameof(PurchaseOrder.ShippingCountry)},
                    shipping_postal_code AS {nameof(PurchaseOrder.ShippingPostalCode)},
                    required_date AS {nameof(PurchaseOrder.RequiredDate)},
                    purchase_order_amount AS {nameof(PurchaseOrder.PurchaseOrderAmount)},
                    payment_status AS {nameof(PurchaseOrder.PaymentStatus)},
                    purchase_order_notes AS {nameof(PurchaseOrder.PurchaseOrderNotes)},
                    status AS {nameof(PurchaseOrder.Status)},
                    created_on_utc AS {nameof(PurchaseOrder.CreateDate)},
                    ROW_NUMBER() OVER (ORDER BY {orderByClause} {sortOrder}) AS RowNum
                FROM oms.purchase_order_headers
                WHERE
                    purchase_order_no ILIKE '%' || @Search || '%'
                    OR supplier_name ILIKE '%' || @Search || '%'  
                    OR contact_full_name ILIKE '%' || @Search || '%'
                    OR shipping_location_name ILIKE '%' || @Search || '%'
                    OR shipping_address_line1 ILIKE '%' || @Search || '%'
                    OR shipping_address_line2 ILIKE '%' || @Search || '%'
                    OR shipping_town_city ILIKE '%' || @Search || '%'
                    OR shipping_county ILIKE '%' || @Search || '%'
                    OR shipping_country ILIKE '%' || @Search || '%'
                    OR shipping_postal_code ILIKE '%' || @Search || '%'
                    OR purchase_order_notes ILIKE '%' || @Search || '%'
            )
            SELECT 
                p.{nameof(PurchaseOrder.Id)},
                p.{nameof(PurchaseOrder.PurchaseOrderNo)},
                p.{nameof(PurchaseOrder.PurchaseDate)},
                p.{nameof(PurchaseOrder.SupplierName)},
                p.{nameof(PurchaseOrder.ContactFullName)},
                p.{nameof(PurchaseOrder.ContactPhone)},
                p.{nameof(PurchaseOrder.ContactEmail)},
                p.{nameof(PurchaseOrder.ShippingLocationName)},
                p.{nameof(PurchaseOrder.ShippingAddressLine1)},
                p.{nameof(PurchaseOrder.ShippingAddressLine2)},
                p.{nameof(PurchaseOrder.ShippingTownCity)},
                p.{nameof(PurchaseOrder.ShippingCounty)},
                p.{nameof(PurchaseOrder.ShippingCountry)},
                p.{nameof(PurchaseOrder.ShippingPostalCode)},
                p.{nameof(PurchaseOrder.RequiredDate)},
                p.{nameof(PurchaseOrder.PurchaseOrderAmount)},
                p.{nameof(PurchaseOrder.PaymentStatus)},
                p.{nameof(PurchaseOrder.PurchaseOrderNotes)},
                p.{nameof(PurchaseOrder.Status)},
                p.{nameof(PurchaseOrder.CreateDate)}
            FROM FilteredPurchaseOrders p
            WHERE p.RowNum BETWEEN ((@Page) * @Size) + 1 AND (@Page + 1) * @Size
            ORDER BY p.RowNum;

            SELECT Count(*) 
                FROM oms.purchase_order_headers
                WHERE
                    purchase_order_no ILIKE '%' || @Search || '%'
                    OR supplier_name ILIKE '%' || @Search || '%'  
                    OR contact_full_name ILIKE '%' || @Search || '%'
                    OR shipping_location_name ILIKE '%' || @Search || '%'
                    OR shipping_address_line1 ILIKE '%' || @Search || '%'
                    OR shipping_address_line2 ILIKE '%' || @Search || '%'
                    OR shipping_town_city ILIKE '%' || @Search || '%'
                    OR shipping_county ILIKE '%' || @Search || '%'
                    OR shipping_country ILIKE '%' || @Search || '%'
                    OR shipping_postal_code ILIKE '%' || @Search || '%'
                    OR purchase_order_notes ILIKE '%' || @Search || '%'
        ";

        SqlMapper.GridReader multi = await connection.QueryMultipleAsync(sql, request);

        var purchaseOrders = (await multi.ReadAsync<PurchaseOrder>()).ToList();
        int totalCount = await multi.ReadSingleAsync<int>();

        int totalPages = (int)Math.Ceiling(totalCount / (double)request.Size);
        int currentPage = request.Page;
        int pageSize = request.Size;
        int startIndex = currentPage * pageSize;
        int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);

        return new PurchaseOrdersResponse(purchaseOrders, new Pagination(totalCount, pageSize, currentPage, totalPages, startIndex, endIndex));
    }
}


