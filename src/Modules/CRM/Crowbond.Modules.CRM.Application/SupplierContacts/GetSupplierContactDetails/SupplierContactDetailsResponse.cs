namespace Crowbond.Modules.CRM.Application.SupplierContacts.GetSupplierContactDetails;

public sealed record SupplierContactDetailsResponse(
    Guid Id,
    Guid SupplierId,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Mobile,
    string Username,
    string Email,
    bool IsPrimary,
    bool IsActive);
