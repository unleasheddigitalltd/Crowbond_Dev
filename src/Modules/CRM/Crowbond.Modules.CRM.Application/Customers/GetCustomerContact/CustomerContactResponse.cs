namespace Crowbond.Modules.CRM.Application.Customers.GetCustomerContact;
public sealed record CustomerContactResponse(Guid Id, string Username, string Email, string FirstName, string LastName);
