using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Application.Pagination;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.PublicApi;
using Crowbond.Modules.OMS.Domain.Customers;
using Dapper;

namespace Crowbond.Modules.OMS.Application.Orders.GetMyOrders;

internal sealed class GetMyOrdersQueryHandler(
    ICustomerApi customerApi,
    IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetMyOrdersQuery, OrdersResponse>
{
    public async Task<Result<OrdersResponse>> Handle(GetMyOrdersQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        CustomerForOrderResponse? customer = await customerApi.GetByContactIdAsync(request.CustomerContactId, cancellationToken);

        if (customer is null)
        {
            return Result.Failure<OrdersResponse>(CustomerErrors.ContactNotFound(request.CustomerContactId));
        }

        string sortOrder = request.Order.Equals("ASC", StringComparison.OrdinalIgnoreCase) ? "ASC" : "DESC";
        string orderByClause = request.Sort switch
        {
            "orderNo" => "order_no",
            "customerAccountNumber" => "customer_account_number",
            "customerBusinessName" => "customer_business_name",
            "shippingDate" => "shipping_date",
            "status" => "status",
            _ => "order_no" // Default sorting
        };

        string sql = $@"
            WITH FilteredOrders AS (
                SELECT
                    id AS {nameof(Order.Id)},
                    customer_id AS {nameof(Order.CustomerId)},
                    order_no AS {nameof(Order.OrderNo)},
                    customer_account_number AS {nameof(Order.CustomerAccountNumber)},
                    customer_business_name AS {nameof(Order.CustomerBusinessName)},
                    shipping_date AS {nameof(Order.ShippingDate)},
                    status AS {nameof(Order.Status)},
                    delivery_charge AS {nameof(Order.DeliveryCharge)},
                    order_amount AS {nameof(Order.OrderAmount)},
                    ROW_NUMBER() OVER (ORDER BY {orderByClause} {sortOrder}) AS RowNum
                FROM oms.order_headers
                WHERE
                    customer_id = '{customer.Id}'
                    AND (order_no ILIKE '%' || @Search || '%'
                    OR customer_account_number ILIKE '%' || @Search || '%'
                    OR customer_business_name ILIKE '%' || @Search || '%')
            )
            SELECT 
                {nameof(Order.Id)},
                {nameof(Order.CustomerId)},
                {nameof(Order.OrderNo)},
                {nameof(Order.CustomerAccountNumber)},
                {nameof(Order.CustomerBusinessName)},
                {nameof(Order.ShippingDate)},
                {nameof(Order.Status)},
                {nameof(Order.DeliveryCharge)},
                {nameof(Order.OrderAmount)}
            FROM FilteredOrders
            WHERE RowNum BETWEEN ((@Page) * @Size) + 1 AND (@Page + 1) * @Size            
            ORDER BY RowNum;

            SELECT Count(*) 
            FROM oms.order_headers
            WHERE
                customer_id = '{customer.Id}'
                AND (order_no ILIKE '%' || @Search || '%'
                OR customer_account_number ILIKE '%' || @Search || '%'
                OR customer_business_name ILIKE '%' || @Search || '%')
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
