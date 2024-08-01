using Crowbond.Common.Application.EventBus;

namespace Crowbond.Modules.CRM.IntegrationEvents;

public sealed class CustomerContactUpdatedIntegrationEvent : IntegrationEvent
{
    public CustomerContactUpdatedIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid userId,
        string username,
        string email,
        string firstName,
        string lastName)
        : base(id, occurredOnUtc)
    {
        UserId = userId;
        Username = username;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    public Guid UserId { get; init; }

    public string Username { get; init; }

    public string Email { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }
}
