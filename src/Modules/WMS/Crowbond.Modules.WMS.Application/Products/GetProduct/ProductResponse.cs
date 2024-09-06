namespace Crowbond.Modules.WMS.Application.Products.GetProduct;

public sealed record ProductResponse()
{
    public Guid Id { get; }

    public string Sku { get; }

    public string Name { get; }

    public string UnitOfMeasureName { get; }

    public string CategoryName { get; }

    public decimal Stock { get; }

    public decimal? ReorderLevel { get; }

    public bool IsActive { get; }
};
