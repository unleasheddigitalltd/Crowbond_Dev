namespace Crowbond.Modules.WMS.Domain.Products;

public sealed class InventoryType
{
    public static readonly InventoryType Standard = new("Standard");
    public static readonly InventoryType Exclusive = new("Exclusive");

    public InventoryType(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }
}
