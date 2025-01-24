using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Users;

public sealed class User: Entity
{
    private User()
    {        
    }

    public Guid Id { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string Username { get; private set; }

    public string Email { get; private set; }

    public string Mobile { get; private set; }

    public bool IsActive { get; private set; }


    public static User Create(Guid id, string username, string email, string firstName, string lastName, string mobile)
    {
        var user = new User
        {
            Id = id,
            Username = username,
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            Mobile = mobile,
            IsActive = true,
        };

        return user;
    }

    public void Update(string username, string email, string firstName, string lastName, string mobile)
    {
        Username = username;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Mobile = mobile;
    }

    public void Activate()
    {
        if (IsActive)
        {
            return;
        }

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
