using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.Suppliers;

    public sealed class Supplier : Entity
{
    public Supplier()
    {
    }

    public Guid Id { get; }
    public string AccountNumber { get; private set; }
    public string SupplierName { get; private set; }

    public string AddressLine1 { get; private set; }

    public string? AddressLine2 { get; private set; }

    public string AddressTownCity { get; private set; }

    public string AddressCounty { get; private set; }

    public string AddressCountry { get; private set; }

    public string AddressPostalCode { get; private set; }

    public string BillingAddressLine1 { get; private set; }

    public string? BillingAddressLine2 { get; private set; }

    public string BillingAddressTownCity { get; private set; }

    public string BillingAddressCounty { get; private set; }

    public string BillingAddressCountry { get; private set; }

    public string BillingAddressPostalCode { get; private set; }

    public string EmailAddress { get; private set; }

    public string PhoneNumber { get; private set; }

    public string ContactName { get; private set; }

    public int PaymentTerms { get; private set; }

    public string? SupplierNotes { get; private set; }

}


