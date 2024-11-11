using Crowbond.Modules.CRM.Domain.Customers;
using Crowbond.Modules.CRM.Domain.Settings;

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
    public bool CustomPaymentTerms { get; }
    public DueDateCalculationBasis? DueDateCalculationBasis { get; set; }
    public int? DueDaysForInvoice { get; set; }
    public string? CustomerNotes { get; }
    public int DeliveryFeeSetting { get; }
    public decimal DeliveryMinOrderValue { get; }
    public decimal DeliveryCharge { get; }
}

public sealed record PaymentSetting(PaymentTerms PaymentTerms);
