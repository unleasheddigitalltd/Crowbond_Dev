using System.Xml.Linq;

namespace Crowbond.Modules.CRM.Application.Suppliers.GetSupplierDetails;

public sealed record SupplierDetailsResponse()
{
    public Guid Id { get; }

    public int AccountNumber { get; set; }
    public string SupplierName { get; set; }

    public string AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string TownCity { get; set; }

    public string County { get; set; }

    public string? Country { get; set; }

    public string PostalCode { get; set; }

    public string BillingAddressLine1 { get; set; }

    public string? BillingAddressLine2 { get; set; }

    public string BillingTownCity { get; set; }

    public string BillingCounty { get; set; }

    public string BillingCountry { get; set; }

    public string BillingPostalCode { get; set; }

    public int PaymentTerms { get; set; }

    public string? SupplierNotes { get; set; }

    public string SupplierEmail { get; set; }

    public string SupplierPhone { get; set; }

    public string SupplierContact { get; set; }
};
