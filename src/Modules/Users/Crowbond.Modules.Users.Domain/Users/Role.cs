namespace Crowbond.Modules.Users.Domain.Users;

public sealed class Role
{
    public static readonly Role Administrator = new("Administrator");
    public static readonly Role Customer = new("Customer");
    public static readonly Role Supplier = new("Supplier");
    public static readonly Role Driver = new("Driver");
    public static readonly Role WarehouseOperator = new("WarhouseOperator");
    public static readonly Role WarehouseManager = new("WarhouseManager");

    private Role(string name)
    {
        Name = name;
    }

    private Role()
    {
    }

    public string Name { get; private set; }
}
