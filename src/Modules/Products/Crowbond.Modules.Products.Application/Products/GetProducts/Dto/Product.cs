namespace Crowbond.Modules.Products.Application.Products.GetProducts.Dto;

public sealed record Product()
{
    public Guid Id { get; }

    public string Sku { get; }

    public string Name { get; }

    public string FilterTypeName { get; }

    public string UnitOfMeasureName { get; }

    public string CategoryName { get; }

    public bool Active { get; }       
}
