using Crowbond.Common.Domain;
using Crowbond.Modules.Users.Domain.Users;

namespace Crowbond.Modules.Users.Domain.Users;

public sealed class User : Entity
{
    private readonly List<Role> _roles = [];

    private User()
    {
    }

    public Guid Id { get; private set; }

    public string Username { get; private set; }

    public string Email { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string IdentityId { get; private set; }

    public bool IsActive { get; private set; }

    public IReadOnlyCollection<Role> Roles => _roles.ToList();

    public static User Create(Guid id, string username, string email, string firstName, string lastName, string identityId)
    {
        var user = new User
        {
            Id = id,
            Username = username,
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            IdentityId = identityId,
            IsActive = true,
        };

        user.Raise(new UserRegisteredDomainEvent(user.Id));

        return user;
    }

    public void AddRole(Role role)
    {
        _roles.Add(role);
    }

    public void Update(string firstName, string lastName)
    {
        if (FirstName == firstName && LastName == lastName)
        {
            return;
        }

        FirstName = firstName;
        LastName = lastName;

        Raise(new UserProfileUpdatedDomainEvent(Id, FirstName, LastName));
    }

    public void Activate(string identityId)
    {
        if (IsActive)
        {
            return;
        }
        IdentityId = identityId;
        IsActive = true;
    }

    public void Deactivate()
    {
        if (!IsActive)
        {
            return;
        }

        IsActive = false;
    }
}
