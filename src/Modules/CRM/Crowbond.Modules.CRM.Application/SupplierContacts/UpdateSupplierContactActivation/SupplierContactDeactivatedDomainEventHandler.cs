using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.CRM.Domain.SupplierContacts;
using Crowbond.Modules.CRM.IntegrationEvents;

namespace Crowbond.Modules.CRM.Application.SupplierContacts.UpdateSupplierContactActivation;

internal sealed class SupplierContactDeactivatedDomainEventHandler(IEventBus eventBus)
    : DomainEventHandler<SupplierContactDeactivatedDomainEvent>
{
    public override async Task Handle(
        SupplierContactDeactivatedDomainEvent domainEvent, 
        CancellationToken cancellationToken = default)
    {
        await eventBus.PublishAsync(
            new SupplierContactDeactivatedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                domainEvent.SupplierContactId),
            cancellationToken);
    }
}
