using Crowbond.Modules.OMS.Domain.Products;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrderLine;

public sealed record PurchaseOrderLineResponse(
    Guid Id,
    Guid PurchaseOrderHeaderId,
    Guid ProductId,
    string ProductSku,
    string ProductName,
    string UnitOfMeasureName,
    Guid CategoryId,
    string CategoryName,
    Guid BrandId,
    string BrandName,
    Guid ProductGroupId,
    string ProductGroupName,
    decimal UnitPrice,
    decimal Qty,
    decimal SubTotal,
    TaxRateType TaxRateType,
    decimal Tax,
    decimal LineTotal,
    string? Comments);
