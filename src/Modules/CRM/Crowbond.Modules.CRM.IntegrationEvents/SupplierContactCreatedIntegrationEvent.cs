using Crowbond.Common.Application.EventBus;

namespace Crowbond.Modules.CRM.IntegrationEvents;

public sealed class SupplierContactCreatedIntegrationEvent : IntegrationEvent
{
    public SupplierContactCreatedIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid userId,
        string username,
        string email,
        string firstName,
        string lastName,
        string mobile
        )
        : base(id, occurredOnUtc)
    {
        UserId = userId;
        Username = username;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Mobile = mobile;
    }

    public Guid UserId { get; init; }

    public string Username { get; init; }

    public string Email { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string Mobile { get; init; }
}
