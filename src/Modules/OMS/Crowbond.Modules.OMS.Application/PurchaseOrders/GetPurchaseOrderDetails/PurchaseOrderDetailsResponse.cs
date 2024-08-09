namespace Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrderDetails;

public sealed record PurchaseOrderDetailsResponse
{
    public Guid Id { get; }
    public string? PurchaseOrderNo { get; }
    public DateOnly? PurchaseDate { get; }
    public string? PaidBy { get; }
    public DateTime? PaidDate { get; }
    public Guid SupplierId { get; }
    public string SupplierName { get; }
    public string? ContactFullName { get; }
    public string? ContactPhone { get; }
    public string? ContactEmail { get; }
    public decimal PurchaseOrderAmount { get; }
    public string? ShippingLocationName { get; }
    public string? ShippingAddressLine1 { get; }
    public string? ShippingAddressLine2 { get; }
    public string? ShippingTownCity { get; }
    public string? ShippingCounty { get; }
    public string? ShippingCountry { get; }
    public string? ShippingPostalCode { get; }
    public DateOnly RequiredDate { get; }
    public DateOnly? ExpectedShippingDate { get; }
    public string? SupplierReference { get; }
    public decimal PurchaseOrderTax { get; }
    public int? DeliveryMethod { get; }
    public decimal DeliveryCharge { get; }
    public int PaymentMethod { get; }
    public int PaymentStatus { get; }
    public string? PurchaseOrderNotes { get; }
    public string? SalesOrderRef { get; }
    public string Tags { get; }
    public int Status { get; }
}
