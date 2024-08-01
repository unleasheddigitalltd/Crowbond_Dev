namespace Crowbond.Modules.CRM.Application.CustomerContacts.GetCustomerContactDetails;

public sealed record CustomerContactDetailsResponse(
    Guid Id,    
    Guid CustomerId,    
    string FirstName,    
    string LastName,    
    string PhoneNumber,    
    string Mobile,    
    string Username,    
    string Email,    
    bool Primary,    
    bool ReceiveInvoice,    
    bool ReceiveOrder,    
    bool ReceivePriceList,    
    bool IsActive);
