namespace Crowbond.Modules.CRM.Application.Customers.GetCustomerForOrderByContactId;

public sealed record CustomerForOrderResponse
{
    public Guid Id { get; }
    public string AccountNumber { get; }
    public string BusinessName { get; }
    public Guid PriceTierId { get; }
    public decimal Discount { get; }
    public bool NoDiscountSpecialItem { get; }
    public bool NoDiscountFixedPrice { get; }
    public bool DetailedInvoice { get; }
    public int PaymentTerms { get; }
    public string? CustomerNotes { get; }
    public int DeliveryFeeSetting { get; }
    public decimal DeliveryMinOrderValue { get; }
    public decimal DeliveryCharge { get; }
}
