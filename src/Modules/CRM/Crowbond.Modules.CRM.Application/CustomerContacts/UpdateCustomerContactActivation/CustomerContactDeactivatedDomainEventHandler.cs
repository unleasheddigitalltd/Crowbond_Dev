using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.CRM.Domain.CustomerContacts;
using Crowbond.Modules.CRM.IntegrationEvents;

namespace Crowbond.Modules.CRM.Application.CustomerContacts.UpdateCustomerContactActivation;

internal sealed class CustomerContactDeactivatedDomainEventHandler(IEventBus eventBus)
    : DomainEventHandler<CustomerContactDeactivatedDomainEvent>
{
    public override async Task Handle(
        CustomerContactDeactivatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await eventBus.PublishAsync(
            new CustomerContactDeactivatedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                domainEvent.CustomerContactId),
            cancellationToken);
    }
}
