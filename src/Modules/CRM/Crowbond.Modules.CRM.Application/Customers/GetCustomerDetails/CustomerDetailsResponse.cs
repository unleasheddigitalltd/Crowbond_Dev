namespace Crowbond.Modules.CRM.Application.Customers.GetCustomerDetails;

public sealed record CustomerDetailsResponse
{
    public CustomerDetailsResponse()
    {
        CustomerContacts = new List<CustomerContactResponse>();
        CustomerShippingAddresses = new List<CustomerShippingAddressResponse>();
    }

    public Guid Id { get; }

    public string AccountNumber { get; }

    public string BusinessName { get; }

    public string BillingAddressLine1 { get; }

    public string? BillingAddressLine2 { get; }

    public string BillingTownCity { get; }

    public string BillingPostalCode { get; }

    public string BillingCounty { get; }

    public string BillingCountry { get; }

    public Guid PriceGroupId { get;  }

    public Guid InvoicePeriodId { get; }

    public int PaymentTerms { get; }

    public bool DetailedInvoice { get; }

    public string? CustomerNotes { get; }

    public bool IsHq { get; }

    public bool IsActive { get; }

    public List<CustomerContactResponse> CustomerContacts { get; set; }

    public List<CustomerShippingAddressResponse> CustomerShippingAddresses { get; set; }
};
public sealed record CustomerContactResponse(
    Guid Id,
    Guid CustomerId,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Mobile,
    string Email,
    bool Primary,
    bool IsActive);

public sealed record CustomerShippingAddressResponse(
    Guid Id,
    Guid CustomerId,
    string ShippingAddressLine1,
    string? ShippingAddressLine2,
    string ShippingTownCity,
    string ShippingCounty,
    string? ShippingCountry,
    string ShippingPostalCode,
    string? DeliveryNote,
    DateTime DeliveryTimeFrom,
    DateTime DeliveryTimeTo,
    bool Is24HrsDelivery);

