using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.CRM.Domain.CustomerContacts;
using Crowbond.Modules.CRM.IntegrationEvents;

namespace Crowbond.Modules.CRM.Application.CustomerContacts.UpdateCustomerContactActivation;

internal sealed class CustomerContactActivatedDomainEventHandler(IEventBus eventBus)
    : DomainEventHandler<CustomerContactActivatedDomainEvent>
{
    public override async Task Handle(
        CustomerContactActivatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await eventBus.PublishAsync(
            new CustomerContactActivatedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                domainEvent.CustomerContactId),
            cancellationToken);
    }
}
