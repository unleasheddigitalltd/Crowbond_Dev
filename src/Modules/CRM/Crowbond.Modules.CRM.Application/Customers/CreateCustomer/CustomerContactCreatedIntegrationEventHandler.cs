using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Customers.GetCustomerContact;
using Crowbond.Modules.CRM.Domain.CustomerContacts;
using Crowbond.Modules.CRM.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.CRM.Application.Customers.CreateCustomer;

internal sealed class CustomerContactCreatedIntegrationEventHandler(ISender sender, IEventBus eventBus)
    : DomainEventHandler<CustomerContactCreatedDomainEvent>
{
    public override async Task Handle(
        CustomerContactCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Result<CustomerContactResponse> result = await sender.Send(
            new GetCustomerContactQuery(domainEvent.CustomerContactId),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(GetCustomerContactQuery), result.Error);
        }

        await eventBus.PublishAsync(
            new CustomerContactCreatedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                result.Value.Id,
                result.Value.Username,
                result.Value.Email,
                result.Value.FirstName,
                result.Value.LastName),
            cancellationToken);
    }
}
