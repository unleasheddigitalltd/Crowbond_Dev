using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.Customers;

public sealed class Customer : Entity
{
    public Customer()
    {

    }

    public Guid Id { get; private set; }

    public string AccountNumber { get; private set; }

    public string BusinessName { get; private set; }

    public string? DriverCode { get; private set; }

    public string ShippingAddressLine1 { get; private set; }

    public string? ShippingAddressLine2 { get; private set; }

    public string ShippingTownCity { get; private set; }

    public string ShippingCounty { get; private set; }

    public string? ShippingCountry { get; private set; }

    public string ShippingPostalCode { get; private set; }

    public string BillingAddressLine1 { get; private set; }

    public string? BillingAddressLine2 { get; private set; }

    public string BillingTownCity { get; private set; }

    public string BillingCounty { get; private set; }

    public string BillingCountry { get; private set; }

    public string BillingPostalCode { get; private set; }

    public Guid PriceGroupId { get; private set; }

    public Guid InvoicePeriodId { get; private set; }

    public int PaymentTerms { get; private set; }

    public bool DetailedInvoice { get; private set; }

    public string? CustomerNotes { get; private set; }

    public string CustomerEmail { get; private set; }

    public string CustomerPhone { get; private set; }

    public string CustomerContact { get; private set; }


    public static Result<Customer> Create(
         string accountNumber,
         string businessName,
         string? driverCode,
         string shippingAddressLine1,
         string? shippingAddressLine2,
         string shippingTownCity,
         string shippingCounty,
         string? shippingCountry,
         string shippingPostalCode,
         string billingAddressLine1,
         string? billingAddressLine2,
         string billingTownCity,
         string billingCounty,
         string billingCountry,
         string billingPostalCode,
         Guid priceGroupId,
         int paymentTerms,
         string? customerNotes,
        string customerEmail,
        string customerPhone,
        string customerContact
    )
    {
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            AccountNumber = accountNumber,
            BusinessName = businessName,
            DriverCode = driverCode,
            ShippingAddressLine1 = shippingAddressLine1,
            ShippingAddressLine2 = shippingAddressLine2,
            ShippingTownCity = shippingTownCity,
            ShippingCounty = shippingCounty,
            ShippingCountry = shippingCountry,
            ShippingPostalCode = shippingPostalCode,
            BillingAddressLine1 = billingAddressLine1,
            BillingAddressLine2 = billingAddressLine2,
            BillingTownCity = billingTownCity,
            BillingCounty = billingCounty,
            BillingCountry = billingCountry,
            BillingPostalCode = billingPostalCode,
            PriceGroupId = priceGroupId,
            PaymentTerms = paymentTerms,
            CustomerNotes = customerNotes,
            CustomerEmail = customerEmail,
            CustomerPhone = customerPhone,
            CustomerContact = customerContact
        };

        return customer;
    }

    public void Update(
        string businessName,
        string? driverCode,
        string shippingAddressLine1,
        string? shippingAddressLine2,
        string shippingTownCity,
        string shippingCounty,
        string? shippingCountry,
        string shippingPostalCode,
        string billingAddressLine1,
        string? billingAddressLine2,
        string billingTownCity,
        string billingCounty,
        string billingCountry,
        string billingPostalCode,
        Guid priceGroupId,
        int paymentTerms,
        string? customerNotes,
        string customerEmail,
        string customerPhone,
        string customerContact)
    {

        BusinessName = businessName;
        DriverCode = driverCode;
        ShippingAddressLine1 = shippingAddressLine1;
        ShippingAddressLine2 = shippingAddressLine2;
        ShippingTownCity = shippingTownCity;
        ShippingCounty = shippingCounty;
        ShippingCountry = shippingCountry;
        ShippingPostalCode = shippingPostalCode;
        BillingAddressLine1 = billingAddressLine1;
        BillingAddressLine2 = billingAddressLine2;
        BillingTownCity = billingTownCity;
        BillingCounty = billingCounty;
        BillingCountry = billingCountry;
        BillingPostalCode = billingPostalCode;
        PriceGroupId = priceGroupId;
        PaymentTerms = paymentTerms;
        CustomerNotes = customerNotes;
        CustomerEmail = customerEmail;
        CustomerPhone = customerPhone;
        CustomerContact = customerContact;
    }
}


