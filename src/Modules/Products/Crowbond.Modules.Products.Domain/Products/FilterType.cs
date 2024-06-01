namespace Crowbond.Modules.Products.Domain.Products;

public sealed class FilterType
{
    public static readonly FilterType Box = new("Box");
    public static readonly FilterType Case = new("Case");
    public static readonly FilterType Each = new("Each");
    public static readonly FilterType Kg = new("Kg");
    public static readonly FilterType Processed = new("Processed");

    public FilterType(string name)
    {
        Name = name;
    }

    public FilterType()
    {        
    }

    public string Name { get; private set; }
}
