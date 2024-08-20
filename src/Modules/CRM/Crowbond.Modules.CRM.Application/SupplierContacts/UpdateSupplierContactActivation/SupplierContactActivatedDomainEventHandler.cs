using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.CRM.Domain.SupplierContacts;
using Crowbond.Modules.CRM.IntegrationEvents;

namespace Crowbond.Modules.CRM.Application.SupplierContacts.UpdateSupplierContactActivation;

internal sealed class SupplierContactActivatedDomainEventHandler(IEventBus eventBus)
    : DomainEventHandler<SupplierContactActivatedDomainEvent>
{
    public override async Task Handle(
        SupplierContactActivatedDomainEvent domainEvent, 
        CancellationToken cancellationToken = default)
    {
        await eventBus.PublishAsync(
            new SupplierContactActivatedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                domainEvent.SupplierContactId),
            cancellationToken);
    }
}
