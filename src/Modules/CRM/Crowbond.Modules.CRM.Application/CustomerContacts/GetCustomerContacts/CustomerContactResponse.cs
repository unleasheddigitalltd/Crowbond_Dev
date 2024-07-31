namespace Crowbond.Modules.CRM.Application.CustomerContacts.GetCustomerContacts;

public sealed record CustomerContactResponse(
    Guid Id,
    Guid CustomerId,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Mobile,
    string Email,
    string Username,
    bool Primary,
    bool ReceiveInvoice,
    bool ReceiveOrder,
    bool ReceivePriceList,
    bool IsActive);
