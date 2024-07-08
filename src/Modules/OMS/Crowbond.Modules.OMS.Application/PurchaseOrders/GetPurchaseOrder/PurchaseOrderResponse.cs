namespace Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrder;

public sealed record PurchaseOrderResponse()
{
    public Guid Id { get; }

    public string PurchaseOrderNumber { get; set; }
    public string SupplierName { get; set; }

    public string AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string AddressTownCity { get; set; }

    public string AddressCounty { get; set; }

    public string? AddressCountry { get; set; }

    public string AddressPostalCode { get; set; }

    public string BillingAddressLine1 { get; set; }

    public string? BillingAddressLine2 { get; set; }

    public string BillingAddressTownCity { get; set; }

    public string BillingAddressCounty { get; set; }

    public string BillingAddressCountry { get; set; }

    public string BillingAddressPostalCode { get; set; }

    public int PaymentTerms { get; set; }

    public string? SupplierNotes { get; set; }

    public string SupplierEmail { get; set; }

    public string SupplierPhone { get; set; }

    public string SupplierContact { get; set; }
};
