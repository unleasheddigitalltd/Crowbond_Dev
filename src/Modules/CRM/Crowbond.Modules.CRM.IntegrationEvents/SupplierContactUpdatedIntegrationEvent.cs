using Crowbond.Common.Application.EventBus;

namespace Crowbond.Modules.CRM.IntegrationEvents;

public sealed class SupplierContactUpdatedIntegrationEvent : IntegrationEvent
{
    public SupplierContactUpdatedIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid userId,
        string firstName,
        string lastName)
        : base(id, occurredOnUtc)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
    }

    public Guid UserId { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }
}
