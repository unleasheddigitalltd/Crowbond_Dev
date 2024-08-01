namespace Crowbond.Modules.CRM.Application.SupplierContacts.UpdateSupplierContact;

public sealed record SupplierContactRequest(
    string FirstName,
    string LastName,
    string PhoneNumber,
    string? Mobile,
    bool Primary,
    bool IsActive);
