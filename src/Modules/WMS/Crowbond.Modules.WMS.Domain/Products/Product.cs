using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Products;

public sealed class Product : Entity
{
    private Product()
    {
    }

    public Guid Id { get; private set; }

    public string Sku { get; private set; }

    public string Name { get; private set; }

    public Guid? ParentId { get; private set; }

    public string FilterTypeName { get; private set; }

    public string UnitOfMeasureName { get; private set; }

    public string InventoryTypeName { get; private set; }

    public Guid CategoryId { get; private set; }
    public Guid? DefaultLocation { get; private set; }

    public Guid BrandId { get; private set; }

    public Guid ProductGroupId { get; private set; }

    public TaxRateType TaxRateType { get; private set; }

    public int? Barcode { get; private set; }

    public decimal? PackSize { get; private set; }

    public string? HandlingNotes { get; private set; }

    public bool QiCheck { get; private set; }

    public string? Notes { get; private set; }

    public decimal? ReorderLevel { get; private set; }

    public decimal? Height { get; private set; }

    public decimal? Width { get; private set; }

    public decimal? Length { get; private set; }

    public bool WeightInput { get; private set; }

    public bool IsActive { get; private set; }

    public static Result<Product> Create(
        string sku,
        string name,
        Guid? parentId,
        string filterTypeName,
        string unitOfMeasureName,
        string inventoryTypeName,
        Guid categoryId,
        Guid defaultLocation,
        Guid brandId,
        Guid productGroupId,
        TaxRateType taxRateType,
        int? barcode,
        decimal? packSize,
        string? handlingNote,
        bool qiCheck,
        string? notes,
        decimal? reorderLevel,
        decimal? height,
        decimal? width,
        decimal? length,
        bool weightInput)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Sku = sku,
            Name = name,
            ParentId = parentId,
            FilterTypeName = filterTypeName,
            UnitOfMeasureName = unitOfMeasureName,
            InventoryTypeName = inventoryTypeName,
            CategoryId = categoryId,
            BrandId = brandId,
            ProductGroupId = productGroupId,
            TaxRateType = taxRateType,
            Barcode = barcode,
            PackSize = packSize,
            HandlingNotes = handlingNote,
            QiCheck = qiCheck,
            Notes = notes,
            ReorderLevel = reorderLevel,
            Height = height,
            Width = width,
            Length = length,
            WeightInput = weightInput,
            IsActive = true
        };

        product.Raise(new ProductCreatedDomainEvent(product.Id));

        return product;
    }

    public void Update(
        string sku,
        string name,
        Guid? parentId,
        string filterTypeName,
        string unitOfMeasureName,
        string inventoryTypeName,
        Guid categoryId,
        Guid defaultLocation,
        Guid brandId,
        Guid productGroupId,
        TaxRateType taxRateType,
        int? barcode,
        decimal? packSize,
        string? handlingNotes,
        bool qiCheck,
        string? notes,
        decimal? reorderLevel,
        decimal? height,
        decimal? width,
        decimal? length,
        bool weightInput,
        bool isActive)
    {
        Sku = sku;
        Name = name;
        ParentId = parentId;
        FilterTypeName = filterTypeName;
        UnitOfMeasureName = unitOfMeasureName;
        InventoryTypeName = inventoryTypeName;
        CategoryId = categoryId;
        DefaultLocation = defaultLocation;
        BrandId = brandId;
        ProductGroupId = productGroupId;
        TaxRateType = taxRateType;
        Barcode = barcode;
        PackSize = packSize;
        HandlingNotes = handlingNotes;
        QiCheck = qiCheck;
        Notes = notes;
        ReorderLevel = reorderLevel;
        Height = height;
        Width = width;
        Length = length;
        WeightInput = weightInput;
        IsActive = isActive;

        Raise(new ProductUpdatedDomainEvent(Id));
    }
}
