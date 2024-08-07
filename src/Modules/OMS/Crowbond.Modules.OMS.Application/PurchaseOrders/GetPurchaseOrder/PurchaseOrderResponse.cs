using Crowbond.Modules.OMS.Domain.Payments;
using Crowbond.Modules.OMS.Domain.PurchaseOrderHeaders;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrder;

public sealed record PurchaseOrderResponse
{
    public Guid Id { get; }
    public string? PurchaseOrderNo { get; }
    public DateOnly? PurchaseDate { get; }
    public string SupplierName { get; }
    public string? ContactFullName { get; }
    public string? ContactPhone { get; }
    public string? ContactEmail { get; }
    public string? ShippingLocationName { get; }
    public string? ShippingAddressLine1 { get; }
    public string? ShippingAddressLine2 { get; }
    public string? ShippingTownCity { get; }
    public string? ShippingCounty { get; }
    public string? ShippingCountry { get; }
    public string? ShippingPostalCode { get; }
    public DateOnly RequiredDate { get; }
    public decimal PurchaseOrderAmount { get; }
    public int PaymentStatus { get; }
    public string? PurchaseOrderNotes { get; }
    public int Status { get; }
    public DateTime CreateDate { get; }
}

