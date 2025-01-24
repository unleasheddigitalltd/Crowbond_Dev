using Crowbond.Common.Domain;

namespace Crowbond.Modules.Users.Domain.Users;

public sealed class User : Entity, IChangeDetectable
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

    public string Mobile { get; private set; }

    public string IdentityId { get; private set; }

    public bool IsActive { get; private set; }

    public IReadOnlyCollection<Role> Roles => _roles;

    public static User Create(Guid id, string username, string email, string firstName, string lastName, string mobile, string identityId)
    {
        var user = new User
        {
            Id = id,
            Username = username,
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            Mobile = mobile,
            IdentityId = identityId,
            IsActive = true,
        };

        user.Raise(new UserRegisteredDomainEvent(user.Id));

        return user;
    }

    public Result AddRole(Role role)
    {
        if (_roles.Contains(role))
        {
            return Result.Failure(UserErrors.UserRoleAlreadyExist(role.Name));
        }

        if (role == Role.Customer || role == Role.Supplier)
        {
            return Result.Failure(UserErrors.InvalidRole(role.Name));
        }

        _roles.Add(role);

        switch (role.Name)
        {
            case "Driver":
                Raise(new DriverRoleAddedDomainEvent(Id));
                break;
            case "WarehouseOperator":
                Raise(new WarehouseOperatorRoleAddedDomainEvent(Id));
                break;
            case "WarehouseManager":
                // No action needed for WarehouseManager
                break;
            default:
                return Result.Failure(UserErrors.UnhandledRole(role.Name));
        }

        return Result.Success();
    }
    
    public Result RemoveRole(Role role)
    {
        if (!_roles.Contains(role))
        {
            return Result.Failure(UserErrors.UserRoleNotExist(role.Name));
        }

        if (role == Role.Customer || role == Role.Supplier)
        {
            return Result.Failure(UserErrors.InvalidRole(role.Name));
        }

        _roles.Remove(role);

        switch (role.Name)
        {
            case "Driver":
                Raise(new DriverRoleRemovedDomainEvent(Id));
                break;
            case "WarehouseOperator":
                Raise(new WarehouseOperatorRoleRemovedDomainEvent(Id));
                break;
            case "WarehouseManager":
                // No action needed for WarehouseManager
                break;
            default:
                return Result.Failure(UserErrors.UnhandledRole(role.Name));
        }

        return Result.Success();
    }


    public void Update(string username, string email, string firstName, string lastName, string mobile)
    {
        Username = username;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Mobile = mobile;

        Raise(new UserProfileUpdatedDomainEvent(Id, Username, Email, FirstName, LastName, Mobile));
    }

    public void Activate()
    {
        if (IsActive)
        {
            return;
        }

        IsActive = true;

        Raise(new UserActivatedDomainEvent(Id));
    }

    public void Deactivate()
    {
        if (!IsActive)
        {
            return;
        }

        IsActive = false;

        Raise(new UserDeactivatedDomainEvent(Id));
    }
}
