using Crowbond.Common.Application.Pagination;
using Crowbond.Modules.OMS.Domain.Payments;
using Crowbond.Modules.OMS.Domain.PurchaseOrders;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrders;

public sealed class PurchaseOrdersResponse : PaginatedResponse<PurchaseOrder>
{

    public PurchaseOrdersResponse(IReadOnlyCollection<PurchaseOrder> PurchaseOrders, IPagination pagination)
        : base(PurchaseOrders, pagination)
    { }

}
public sealed record PurchaseOrder(
    Guid Id,
    string? PurchaseOrderNo,
    DateOnly? PurchaseDate,
    string SupplierName,
    string? ContactFullName,
    string? ContactPhone,
    string? ContactEmail,
    string? ShippingLocationName,
    string? ShippingAddressLine1,
    string? ShippingAddressLine2,
    string? ShippingTownCity,
    string? ShippingCounty,
    string? ShippingCountry,
    string? ShippingPostalCode,
    DateOnly RequiredDate,
    decimal PurchaseOrderAmount,
    PaymentStatus PaymentStatus,
    string? PurchaseOrderNotes,
    PurchaseOrderStatus Status,
    DateTime CreateDate);




