using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Application.Pagination;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.OMS.Application.Orders.GetOrders;

internal sealed class GetOrdersQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetOrdersQuery, OrdersResponse>
{
    public async Task<Result<OrdersResponse>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string sortOrder = request.Order.Equals("ASC", StringComparison.OrdinalIgnoreCase) ? "ASC" : "DESC";
        string orderByClause = request.Sort switch
        {
            "orderNo" => "order_no",
            "accountNo" => "account_no",
            "businessName" => "usiness_name",
            "shippingDate" => "shipping_date",
            "status" => "status",
            _ => "order_no" // Default sorting
        };

        string sql = $@"
            WITH FilteredOrders AS (
                SELECT
                    id AS {nameof(Order.Id)},
                    id AS {nameof(Order.CustomerId)},
                    order_no AS {nameof(Order.OrderNo)},
                    account_no AS {nameof(Order.AccountNo)},
                    business_name AS {nameof(Order.BusinessName)},
                    shipping_date AS {nameof(Order.ShippingDate)},
                    status AS {nameof(Order.Status)},
                    delivery_charge AS {nameof(Order.DeliveryCharge)},
                    total_price AS {nameof(Order.TotalPrice)},
                    ROW_NUMBER() OVER (ORDER BY {orderByClause} {sortOrder}) AS RowNum
                FROM oms.orders p
                WHERE
                    o.order_no ILIKE '%' || @Search || '%'
                    OR c.account_no ILIKE '%' || @Search || '%'
                    OR c.business_name ILIKE '%' || @Search || '%'
            )
            SELECT 
                {nameof(Order.Id)},
                {nameof(Order.CustomerId)},
                {nameof(Order.OrderNo)},
                {nameof(Order.AccountNo)},
                {nameof(Order.BusinessName)},
                {nameof(Order.ShippingDate)},
                {nameof(Order.Status)},
                {nameof(Order.DeliveryCharge)},
                {nameof(Order.TotalPrice)}
            FROM FilteredOrders
            WHERE RowNum BETWEEN ((@Page) * @Size) + 1 AND (@Page + 1) * @Size            
            ORDER BY RowNum;

            SELECT Count(*) 
                FROM oms.orders p
                WHERE
                    o.order_no ILIKE '%' || @Search || '%'
                    OR c.account_no ILIKE '%' || @Search || '%'
                    OR c.business_name ILIKE '%' || @Search || '%'
        ";

        SqlMapper.GridReader multi = await connection.QueryMultipleAsync(sql, request);

        var orders = (await multi.ReadAsync<Order>()).ToList();
        int totalCount = await multi.ReadSingleAsync<int>();

        int totalPages = (int)Math.Ceiling(totalCount / (double)request.Size);
        int currentPage = request.Page;
        int pageSize = request.Size;
        int startIndex = (currentPage - 1) * pageSize;
        int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);

        return new OrdersResponse(orders, new Pagination(totalCount, pageSize, currentPage, totalPages, startIndex, endIndex));
    }
}
