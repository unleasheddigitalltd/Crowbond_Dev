using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.CustomerContacts;

public sealed class CustomerContactActivatedDomainEvent(Guid customerContactId) : DomainEvent
{
    public Guid CustomerContactId { get; init; } = customerContactId;
}
