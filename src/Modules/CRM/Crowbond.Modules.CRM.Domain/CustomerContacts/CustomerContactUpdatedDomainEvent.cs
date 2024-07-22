using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.CustomerContacts;

public sealed class CustomerContactUpdatedDomainEvent(Guid customerContactId, string firstName, string lastName) : DomainEvent
{
    public Guid CustomerContactId { get; init; } = customerContactId;
    public string FirstName { get; init; } = firstName;
    public string LastName { get; init; } = lastName;
}
