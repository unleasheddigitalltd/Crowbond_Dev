using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.CustomerContacts.GetCustomerContactDetails;
using Crowbond.Modules.CRM.Domain.CustomerContacts;
using Crowbond.Modules.CRM.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.CRM.Application.CustomerContacts.UpdateCustomerContact;

internal sealed class CustomerContactUpdatedIntegrationEventHandler(ISender sender, IEventBus eventBus)
    : DomainEventHandler<CustomerContactUpdatedDomainEvent>
{
    public override async Task Handle(
        CustomerContactUpdatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Result<CustomerContactDetailsResponse> result = await sender.Send(
            new GetCustomerContactDetailsQuery(domainEvent.CustomerContactId),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(GetCustomerContactDetailsQuery), result.Error);
        }

        await eventBus.PublishAsync(
            new CustomerContactUpdatedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                result.Value.Id,
                result.Value.FirstName,
                result.Value.LastName),
            cancellationToken);
    }
}
