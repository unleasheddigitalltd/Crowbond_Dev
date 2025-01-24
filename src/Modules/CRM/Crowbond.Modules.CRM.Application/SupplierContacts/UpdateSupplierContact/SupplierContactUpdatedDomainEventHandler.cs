using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.SupplierContacts.GetSupplierContactDetails;
using Crowbond.Modules.CRM.Domain.SupplierContacts;
using Crowbond.Modules.CRM.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.CRM.Application.SupplierContacts.UpdateSupplierContact;

internal sealed class SupplierContactUpdatedDomainEventHandler(ISender sender, IEventBus eventBus)
    : DomainEventHandler<SupplierContactUpdatedDomainEvent>
{
    public override async Task Handle(
        SupplierContactUpdatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Result<SupplierContactDetailsResponse> result = await sender.Send(
            new GetSupplierContactDetailsQuery(domainEvent.SupplierContactId),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(GetSupplierContactDetailsQuery), result.Error);
        }

        await eventBus.PublishAsync(
            new SupplierContactUpdatedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                result.Value.Id,
                result.Value.Username,
                result.Value.Email,
                result.Value.FirstName,
                result.Value.LastName,
                result.Value.Mobile),
            cancellationToken);
    }
}
