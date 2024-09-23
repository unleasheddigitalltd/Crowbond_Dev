using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.PublicApi;
using Crowbond.Modules.OMS.Domain.Customers;
using Crowbond.Modules.OMS.Domain.Orders;
using Dapper;

namespace Crowbond.Modules.OMS.Application.Orders.GetMyOrder;

internal sealed class GetMyOrderQueryHandler(
    ICustomerApi customerApi,
    IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetMyOrderQuery, OrderResponse>
{
    public async Task<Result<OrderResponse>> Handle(GetMyOrderQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        CustomerForOrderResponse? customer = await customerApi.GetByContactIdAsync(request.CustomerContactId, cancellationToken);

        if (customer is null)
        {
            return Result.Failure<OrderResponse>(CustomerErrors.ContactNotFound(request.CustomerContactId));
        }

        string sql =
            $"""
             SELECT
                 id AS {nameof(OrderResponse.Id)},
                 customer_id AS {nameof(OrderResponse.CustomerId)},
                 order_no AS {nameof(OrderResponse.OrderNo)},
                 customer_account_number AS {nameof(OrderResponse.CustomerAccountNumber)},
                 customer_business_name AS {nameof(OrderResponse.CustomerBusinessName)},
                 shipping_date AS {nameof(OrderResponse.ShippingDate)},
                 status AS {nameof(OrderResponse.Status)},
                 delivery_charge AS {nameof(OrderResponse.DeliveryCharge)},
                 order_amount AS {nameof(OrderResponse.OrderAmount)}
             FROM oms.order_headers
             WHERE customer_id = '{customer.Id}' AND id = @OrderHeaderId
             """;

        OrderResponse? order = await connection.QuerySingleOrDefaultAsync<OrderResponse>(sql, request);

        if (order is null)
        {
            return Result.Failure<OrderResponse>(OrderErrors.NotFound(request.OrderHeaderId));
        }

        return order;
    }
}
