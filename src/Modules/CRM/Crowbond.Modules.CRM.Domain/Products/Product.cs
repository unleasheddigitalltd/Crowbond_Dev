using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.Products;

public sealed class Product : Entity
{
    public Product()
    {        
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string Sku { get; private set; }

    public string UnitOfMeasureName { get; private set; }

    public Guid CategoryId { get; private set; }

    public static Product Create (string name, string sku, string unitOfMeasure, Guid categoryId)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
            Sku = sku,
            UnitOfMeasureName = unitOfMeasure,
            CategoryId = categoryId
        };

        return product;
    }

    public void Update (string name, string sku, string unitOfMeasure, Guid categoryId)
    {
        Name = name;
        Sku = sku;
        UnitOfMeasureName = unitOfMeasure;
        CategoryId = categoryId;
    }
}
