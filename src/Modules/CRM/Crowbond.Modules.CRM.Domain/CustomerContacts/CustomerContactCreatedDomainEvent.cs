using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.CustomerContacts;

public sealed class CustomerContactCreatedDomainEvent(Guid customerContactId) : DomainEvent
{
    public Guid CustomerContactId { get; init; } = customerContactId;
}
