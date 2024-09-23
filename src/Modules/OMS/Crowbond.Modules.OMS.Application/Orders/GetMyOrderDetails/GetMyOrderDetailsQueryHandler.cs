using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.PublicApi;
using Crowbond.Modules.OMS.Domain.Customers;
using Crowbond.Modules.OMS.Domain.Orders;
using Dapper;

namespace Crowbond.Modules.OMS.Application.Orders.GetMyOrderDetails;

internal sealed class GetMyOrderDetailsQueryHandler(
    ICustomerApi customerApi,
    IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetMyOrderDetailsQuery, OrderResponse>
{
    public async Task<Result<OrderResponse>> Handle(GetMyOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        CustomerForOrderResponse? customer = await customerApi.GetForOrderAsync(request.CustomerContactId, cancellationToken);

        if (customer is null)
        {
            return Result.Failure<OrderResponse>(CustomerErrors.NotFound(request.CustomerContactId));
        }

        string sql =
            $"""
             SELECT
                 o.id AS {nameof(OrderResponse.Id)},
                 o.order_no AS {nameof(OrderResponse.OrderNo)},
                 o.purchase_order_no AS {nameof(OrderResponse.PurchaseOrderNo)},
                 o.customer_id AS {nameof(OrderResponse.CustomerId)},
                 o.customer_account_number AS {nameof(OrderResponse.CustomerAccountNumber)},
                 o.customer_business_name AS {nameof(OrderResponse.CustomerBusinessName)},
                 o.delivery_location_name AS {nameof(OrderResponse.DeliveryLocationName)},
                 o.delivery_full_name AS {nameof(OrderResponse.DeliveryFullName)},
                 o.delivery_email AS {nameof(OrderResponse.DeliveryEmail)},
                 o.delivery_phone AS {nameof(OrderResponse.DeliveryPhone)},
                 o.delivery_mobile AS {nameof(OrderResponse.DeliveryMobile)},
                 o.delivery_notes AS {nameof(OrderResponse.DeliveryNotes)},
                 o.delivery_address_line1 AS {nameof(OrderResponse.DeliveryAddressLine1)},
                 o.delivery_address_line2 AS {nameof(OrderResponse.DeliveryAddressLine2)},
                 o.delivery_town_city AS {nameof(OrderResponse.DeliveryTownCity)},
                 o.delivery_county AS {nameof(OrderResponse.DeliveryCounty)},
                 o.delivery_country AS {nameof(OrderResponse.DeliveryCountry)},
                 o.delivery_postal_code AS {nameof(OrderResponse.DeliveryPostalCode)},
                 o.shipping_date AS {nameof(OrderResponse.ShippingDate)},
                 o.route_trip_id AS {nameof(OrderResponse.RouteTripId)},
                 o.route_name AS {nameof(OrderResponse.RouteName)},
                 o.delivery_method AS {nameof(OrderResponse.DeliveryMethod)},
                 o.delivery_charge AS {nameof(OrderResponse.DeliveryCharge)},
                 o.order_amount AS {nameof(OrderResponse.OrderAmount)},
                 o.order_tax AS {nameof(OrderResponse.OrderTax)},
                 o.payment_status AS {nameof(OrderResponse.PaymentStatus)},
                 o.payment_term AS {nameof(OrderResponse.PaymentTerm)},
                 o.payment_method AS {nameof(OrderResponse.PaymentMethod)},
                 o.payment_due_date AS {nameof(OrderResponse.PaymentDueDate)},
                 o.customer_comment AS {nameof(OrderResponse.CustomerComment)},
                 o.original_source AS {nameof(OrderResponse.OriginalSource)},
                 o.external_order_ref AS {nameof(OrderResponse.ExternalOrderRef)},
                 o.tags AS {nameof(OrderResponse.Tags)},
                 o.status AS {nameof(OrderResponse.Status)},
                 ol.id AS {nameof(OrderLineResponse.OrderLineId)},
                 ol.order_header_id AS {nameof(OrderLineResponse.OrderHeaderId)},
                 ol.product_id AS {nameof(OrderLineResponse.ProductId)},
                 ol.product_sku AS {nameof(OrderLineResponse.ProductSku)},
                 ol.product_name AS {nameof(OrderLineResponse.ProductName)},
                 ol.unit_of_measure_name AS {nameof(OrderLineResponse.UnitOfMeasureName)},
                 ol.unit_price AS {nameof(OrderLineResponse.UnitPrice)},
                 ol.qty AS {nameof(OrderLineResponse.Qty)},
                 ol.sub_total AS {nameof(OrderLineResponse.SubTotal)},
                 ol.tax AS {nameof(OrderLineResponse.Tax)},
                 ol.line_total AS {nameof(OrderLineResponse.LineTotal)},
                 ol.tax_rate_type AS {nameof(OrderLineResponse.TaxRateType)},
                 ol.status AS {nameof(OrderLineResponse.LineStatus)}
             FROM oms.order_headers o
             JOIN oms.order_lines ol ON ol.order_header_id = o.id
             WHERE o.customer_id = '{customer.Id}' AND o.id = @OrderHeaderId
             """;

        Dictionary<Guid, OrderResponse> ordersDictionary = [];
        await connection.QueryAsync<OrderResponse, OrderLineResponse, OrderResponse>(
            sql,
            (order, orderLine) =>
            {
                if (ordersDictionary.TryGetValue(order.Id, out OrderResponse? existingEvent))
                {
                    order = existingEvent;
                }
                else
                {
                    ordersDictionary.Add(order.Id, order);
                }

                order.OrderLines.Add(orderLine);

                return order;
            },
            request,
            splitOn: nameof(OrderLineResponse.OrderLineId));

        if (!ordersDictionary.TryGetValue(request.OrderHeaderId, out OrderResponse orderResponse))
        {
            return Result.Failure<OrderResponse>(OrderErrors.NotFound(request.OrderHeaderId));
        }

        return orderResponse;
    }
}
