using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.Products;

public sealed class Product : Entity
{
    private Product()
    {        
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string Sku { get; private set; }
    
    public string FilterTypeName { get; private set; }

    public string UnitOfMeasureName { get; private set; }

    public string InventoryTypeName { get; private set; }

    public Guid CategoryId { get; private set; }
    
    public Guid BrandId { get; private set; }
    
    public Guid ProductGroupId { get; private set; }

    public TaxRateType TaxRateType { get; private set; }

    public bool IsActive { get; private set; }

    public static Product Create(
        Guid id,
        string name,
        string sku,
        string filterTypeName,
        string unitOfMeasure,
        string inventoryTypeName,
        Guid categoryId,
        Guid brandId,
        Guid productGroupId,
        TaxRateType taxRateType,
        bool isActive)
    {
        var product = new Product
        {
            Id = id,
            Name = name,
            Sku = sku,
            FilterTypeName = filterTypeName,
            UnitOfMeasureName = unitOfMeasure,
            InventoryTypeName = inventoryTypeName,
            CategoryId = categoryId,
            BrandId = brandId,
            ProductGroupId = productGroupId,
            TaxRateType = taxRateType,
            IsActive = isActive
        };

        return product;
    }

    public void Update(
        string name,
        string sku,
        string filterTypeName,
        string unitOfMeasureName,
        string inventoryTypeName,
        Guid categoryId,
        Guid brandId,
        Guid productGroupId,
        TaxRateType taxRateType,
        bool isActive)
    {
        Name = name;
        Sku = sku;
        FilterTypeName = filterTypeName;
        UnitOfMeasureName = unitOfMeasureName;
        InventoryTypeName = inventoryTypeName;
        CategoryId = categoryId;
        BrandId = brandId;
        ProductGroupId = productGroupId;
        TaxRateType = taxRateType;
        IsActive = isActive;
    }
}
