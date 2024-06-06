namespace Crowbond.Modules.Products.Application.Products.GetProducts.Dto;

public sealed record Product()
{
    public Guid Id { get; }

    public string Sku { get; }

    public string Name { get; }

    public string FilterType { get; }

    public string UnitOfMeasure { get; }

    public string Category { get; }

    public bool Active { get; }       
}
