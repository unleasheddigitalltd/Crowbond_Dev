namespace Crowbond.Modules.CRM.Application.SupplierContacts.GetSupplierContact;

public sealed record SupplierContactResponse(
    Guid Id,
    Guid SupplierId,
    string FirstName,
    string LastName,
    string PhoneNumber,
    bool IsPrimary,
    bool IsActive);
