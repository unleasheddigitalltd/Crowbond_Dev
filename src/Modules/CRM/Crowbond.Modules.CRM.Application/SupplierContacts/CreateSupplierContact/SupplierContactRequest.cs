namespace Crowbond.Modules.CRM.Application.SupplierContacts.CreateSupplierContact;

public sealed record SupplierContactRequest(
    string FirstName,    
    string LastName,    
    string PhoneNumber,    
    string? Mobile,    
    string Username,    
    string Email);
