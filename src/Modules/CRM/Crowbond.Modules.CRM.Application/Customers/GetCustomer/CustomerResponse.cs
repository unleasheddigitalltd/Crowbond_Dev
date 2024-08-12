namespace Crowbond.Modules.CRM.Application.Customers.GetCustomer;

public sealed record CustomerResponse()
{
    public Guid Id { get; }
    public string AccountNumber { get; }
    public string BusinessName { get; }
    public string BillingAddressLine1 { get; }
    public string? BillingAddressLine2 { get; }
    public string BillingTownCity { get; }
    public string BillingCounty { get; }
    public string? BillingCountry { get; }
    public string BillingPostalCode { get; }
    public string? FirstName { get; }
    public string? LastName { get; }
    public string? PhoneNumber { get; }
    public string? Email { get; }
    public string IsActive { get; }
};
