namespace Crowbond.Modules.WMS.Domain.Products;

public sealed class UnitOfMeasure
{
    public static readonly UnitOfMeasure Box = new("Box");
    public static readonly UnitOfMeasure Bag = new("Bag");
    public static readonly UnitOfMeasure Each = new("Each");
    public static readonly UnitOfMeasure Kg = new("Kg");
    public static readonly UnitOfMeasure Pack = new("Pack");

    public UnitOfMeasure(string name)
    {
        Name = name;
    }

    public UnitOfMeasure()
    {
    }

    public string Name { get; private set; }
}
