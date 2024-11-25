using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Domain.Products;

namespace Crowbond.Modules.OMS.Application.Orders.GetOrderLines;

public sealed record OrderLineResponse(
    Guid OrderLineId,
    Guid OrderHeaderId,
    Guid ProductId,
    string ProductSku,
    string ProductName,
    string UnitOfMeasureName,
    decimal UnitPrice,
    decimal OrderedQty,
    decimal? ActualQty,
    decimal? DeliveredQty,
    decimal SubTotal,
    decimal Tax,
    decimal LineTotal,
    decimal? DeductionSubTotal,
    decimal? DeductionTax,
    decimal? DeductionLineTotal,
    TaxRateType TaxRateType,
    OrderLineStatus LineStatus);
