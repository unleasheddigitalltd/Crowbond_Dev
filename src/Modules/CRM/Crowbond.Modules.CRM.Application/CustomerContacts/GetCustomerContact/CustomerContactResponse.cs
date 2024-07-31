namespace Crowbond.Modules.CRM.Application.CustomerContacts.GetCustomerContact;
public sealed record CustomerContactResponse(
    Guid Id,
    Guid CustomerId,
    string FirstName, 
    string LastName,
    string PhoneNumber,
    bool Primary,
    bool IsActive);
