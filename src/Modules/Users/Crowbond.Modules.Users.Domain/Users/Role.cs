namespace Crowbond.Modules.Users.Domain.Users;

public sealed class Role
{
    public static readonly Role Administrator = new("Administrator");
    public static readonly Role Customer = new("Customer");
    public static readonly Role Supplier = new("Supplier");
    public static readonly Role Driver = new("Driver");
    public static readonly Role WarehouseOperator = new("WarehouseOperator");
    public static readonly Role WarehouseManager = new("WarhouseManager");

    private Role(string name)
    {
        Name = name;
    }

    private Role()
    {
    }

    public string Name { get; private set; }

    // Static property to hold all roles
    public static IReadOnlyList<Role> AllRoles => new List<Role>
    {
        Administrator,
        Customer,
        Supplier,
        Driver,
        WarehouseOperator,
        WarehouseManager
    };

    // Method to validate and return the matching role instance
    public static Role? GetRoleByName(string roleName)
    {
        return AllRoles.FirstOrDefault(role => role.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase));
    }
}
