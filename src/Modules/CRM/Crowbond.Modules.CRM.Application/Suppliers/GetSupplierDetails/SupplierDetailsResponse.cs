using Crowbond.Modules.CRM.Domain.Customers;

namespace Crowbond.Modules.CRM.Application.Suppliers.GetSupplierDetails;

public sealed record SupplierDetailsResponse
{
    public SupplierDetailsResponse()
    {
        SupplierContacts = new List<SupplierContactResponse>();
    }

    public Guid Id { get; }
    public string AccountNumber { get; }
    public string SupplierName { get; }
    public string AddressLine1 { get; }
    public string? AddressLine2 { get; }
    public string TownCity { get; }
    public string County { get; }
    public string? Country { get; }
    public string PostalCode { get; }
    public PaymentTerm PaymentTerms { get; }
    public string? SupplierNotes { get; }
    public bool IsActive { get; }

    public List<SupplierContactResponse> SupplierContacts { get; set; }
}

public sealed record SupplierContactResponse(
    Guid Id,
    Guid SupplierId,
    string FirstName,
    string LastName,
    string PhoneNumber,
    bool Primary,
    bool IsActive);
