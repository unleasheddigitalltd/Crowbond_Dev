using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.WarehouseOperators;

public sealed class WarehouseOperator : Entity
{
    private WarehouseOperator() { }

    public Guid Id { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string Username { get; private set; }

    public string Email { get; private set; }

    public string Mobile { get; private set; }

    public static WarehouseOperator Create(
        string firstName,
        string lastName,
        string username,
        string email,
        string mobile)
    {

        var @operator = new WarehouseOperator
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Username = username,
            Email = email,
            Mobile = mobile
        };

        @operator.Raise(new WarehouseOperatorCreatedDomainEvent(@operator.Id));

        return @operator;
    }

    public void Update(
        string firstName,
        string lastName,
        string mobile)
    {
        FirstName = firstName;
        LastName = lastName;
        Mobile = mobile;
    }
}
