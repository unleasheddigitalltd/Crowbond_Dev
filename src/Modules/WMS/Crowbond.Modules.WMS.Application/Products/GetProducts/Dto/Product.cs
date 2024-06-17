namespace Crowbond.Modules.WMS.Application.Products.GetProducts.Dto;

public sealed record Product()
{
    public Guid Id { get; }

    public string Sku { get; }

    public string Name { get; }

    public string UnitOfMeasure { get; }

    public string Category { get; }

    public decimal Stock {  get; }

    public decimal? ReorderLevel { get; }

    public bool Active { get; }
}
