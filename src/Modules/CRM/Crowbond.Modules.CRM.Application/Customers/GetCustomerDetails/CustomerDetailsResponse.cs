using Crowbond.Modules.CRM.Domain.Customers;
using Crowbond.Modules.CRM.Domain.CustomerSettings;

namespace Crowbond.Modules.CRM.Application.Customers.GetCustomerDetails;

public sealed record CustomerDetailsResponse
{
    public CustomerDetailsResponse()
    {
        CustomerContacts = new List<CustomerContactResponse>();
        CustomerOutlets = new List<CustomerOutletResponse>();
    }

    public Guid Id { get; }

    public string AccountNumber { get; }

    public string BusinessName { get; }

    public string BillingAddressLine1 { get; }

    public string? BillingAddressLine2 { get; }

    public string BillingTownCity { get; }

    public string BillingCounty { get; }

    public string BillingCountry { get; }

    public string BillingPostalCode { get; }

    public Guid PriceTierId { get; }

    public decimal Discount { get; }

    public Guid RepId { get; }

    public bool CustomPaymentTerm { get; }

    public PaymentTerm? PaymentTerms { get; }

    public int? InvoiceDueDays { get; }

    public DeliveryFeeSetting DeliveryFeeSetting { get; }

    public decimal? DeliveryMinOrderValue { get; }

    public decimal? DeliveryCharge { get; }

    public bool NoDiscountSpecialItem { get; }

    public bool NoDiscountFixedPrice { get; }

    public bool DetailedInvoice { get; }

    public string? CustomerNotes { get; }

    public bool IsHq { get; }

    public bool IsActive { get; }

    public bool ShowPricesInDeliveryDocket { get; }

    public bool ShowPriceInApp { get; }

    public ShowLogoInDeliveryDocket ShowLogoInDeliveryDocket { get; }

    public string? CustomerLogo { get; }

    public List<CustomerContactResponse> CustomerContacts { get; set; }

    public List<CustomerOutletResponse> CustomerOutlets { get; set; }
};
public sealed record CustomerContactResponse(
    Guid Id,
    Guid CustomerId,
    string FirstName,
    string LastName,
    string PhoneNumber,
    bool Primary,
    bool IsActive);

public sealed record CustomerOutletResponse(
    Guid Id,
    Guid CustomerId,
    string LocationName, 
    string AddressLine1,    
    string? AddressLine2,    
    string TownCity,    
    string County,  
    string? Country,
    string PostalCode);

