using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Domain.Products;

namespace Crowbond.Modules.OMS.Application.Orders.GetMyOrderDetails;

public sealed record OrderLineResponse(
    Guid OrderLineId,
    Guid OrderHeaderId,
    Guid ProductId,
    string ProductSku,
    string ProductName,
    string UnitOfMeasureName,
    decimal UnitPrice,
    decimal Qty,
    decimal SubTotal,
    decimal Tax,
    decimal LineTotal,
    TaxRateType TaxRateType,
    OrderLineStatus LineStatus);
