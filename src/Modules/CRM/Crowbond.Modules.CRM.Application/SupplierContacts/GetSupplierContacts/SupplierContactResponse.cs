namespace Crowbond.Modules.CRM.Application.SupplierContacts.GetSupplierContacts;

public sealed record SupplierContactResponse(
    Guid Id,
    Guid SupplierId,
    string FirstName,
    string LastName,
    string PhoneNumber,
    bool Primary,
    bool IsActive);
