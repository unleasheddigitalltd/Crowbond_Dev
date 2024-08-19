namespace Crowbond.Modules.CRM.Application.CustomerContacts.GetCustomerContacts;

public sealed record CustomerContactResponse
{
    public Guid Id { get; }
    public Guid CustomerId { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string PhoneNumber { get; }
    public string Email { get; }
    public bool Primary { get; }
    public bool IsActive { get; }
}

