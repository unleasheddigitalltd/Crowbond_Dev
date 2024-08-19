namespace Crowbond.Modules.CRM.Application.CustomerContacts.GetCustomerContactDetails;

public sealed record CustomerContactDetailsResponse
{
    public Guid Id { get; }
    public Guid CustomerId { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string PhoneNumber { get; }
    public string Mobile { get; }
    public string Username { get; }
    public string Email { get; }
    public bool Primary { get; }
    public bool ReceiveInvoice { get; }
    public bool ReceiveOrder { get; }
    public bool ReceivePriceList { get; }
    public bool IsActive { get; }
}
