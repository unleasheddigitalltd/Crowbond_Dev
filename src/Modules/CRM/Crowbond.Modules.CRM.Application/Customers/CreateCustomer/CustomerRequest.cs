namespace Crowbond.Modules.CRM.Application.Customers.CreateCustomer;

public sealed record CustomerRequest
{
    public string BusinessName { get; }
    public string BillingAddressLine1 { get; }
    public string? BillingAddressLine2 { get; }
    public string BillingTownCity { get; }
    public string BillingCounty { get; }
    public string BillingCountry { get; }
    public string BillingPostalCode { get; }
    public Guid PriceTierId { get; }
    public decimal Discount { get; }
    public Guid? RepId { get; }
    public bool CustomPaymentTerm { get; }
    public int PaymentTerms { get; }
    public int? InvoiceDueDays { get; }
    public int DeliveryFeeSetting { get; }
    public decimal? DeliveryMinOrderValue { get; }
    public decimal? DeliveryCharge { get; }
    public bool NoDiscountSpecialItem { get; }
    public bool NoDiscountFixedPrice { get; }
    public bool DetailedInvoice { get; }
    public string? CustomerNotes { get; }
    public bool IsHq { get; }
    public bool ShowPricesInDeliveryDocket { get; }
    public bool ShowPriceInApp { get; }
    public int ShowLogoInDeliveryDocket { get; }
    public string? CustomerLogo { get; }
}
