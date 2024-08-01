using Crowbond.Common.Application.EventBus;

namespace Crowbond.Modules.OMS.IntegrationEvents;
public sealed class DriverUpdatedIntegrationEvent : IntegrationEvent
{
    public DriverUpdatedIntegrationEvent(
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
