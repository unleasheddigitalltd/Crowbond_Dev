namespace Crowbond.Modules.CRM.Application.Suppliers.GetSuppliers.Dto;

public sealed record Supplier()
{
    public Guid Id { get; }
    public int AccountNumber { get; set; }
    public string SupplierName { get; set; }

    public string AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string AddressTownCity { get; set; }

    public string AddressCounty { get; set; }

    public string AddressCountry { get; set; }

    public string AddressPostalCode { get; set; }

    public string BillingAddressLine1 { get; set; }

    public string? BillingAddressLine2 { get; set; }

    public string BillingAddressTownCity { get; set; }

    public string BillingAddressCounty { get; set; }

    public string BillingAddressCountry { get; set; }

    public string BillingAddressPostalCode { get; set; }

    public string EmailAddress { get; set; }

    public string PhoneNumber { get; set; }

    public string ContactName { get; set; }

    public int PaymentTerms { get; set; }

    public string? SupplierNotes { get; set; }
}
