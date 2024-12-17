using Crowbond.Common.Domain;

namespace Crowbond.Modules.Users.Domain.Users;

public sealed class UserProfileUpdatedDomainEvent(Guid userId, string username, string email, string firstName, string lastName, string mobile) : DomainEvent
{
    public Guid UserId { get; init; } = userId;

    public string Username { get; init; } = username;

    public string Email { get; init; } = email;

    public string FirstName { get; init; } = firstName;

    public string LastName { get; init; } = lastName;

    public string Mobile { get; init; } = mobile;
}
