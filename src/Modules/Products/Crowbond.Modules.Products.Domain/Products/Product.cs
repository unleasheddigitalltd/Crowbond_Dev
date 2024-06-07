using System.ComponentModel;
using Crowbond.Common.Domain;
using Crowbond.Modules.Products.Domain.Categories;

namespace Crowbond.Modules.Products.Domain.Products;

public sealed class Product : Entity
{
    public Product()
    {
    }

    public Guid Id { get; private set; }

    public string Sku { get; private set; }

    public string Name { get; private set; }

    public Guid? ParentId { get; private set; }

    public string FilterTypeName { get; private set; }

    public string UnitOfMeasureName { get; private set; }

    public Guid CategoryId { get; private set; }

    public string InventoryTypeName { get; private set; }

    public int? Barcode { get; private set; }

    public decimal? PackSize { get; private set; }

    public string HandlingNotes { get; private set; }

    public bool QiCheck { get; private set; }

    public string Notes { get; private set; }

    public decimal? ReorderLevel { get; private set; }

    public decimal? Height { get; private set; }

    public decimal? Width { get; private set; }

    public decimal? Length { get; private set; }

    public bool WeightInput { get; private set; }

    public bool Active { get; private set; }

    public static Result<Product> Create(
        string sku,
        string name,
        Guid? parentId,
        FilterType filterType,
        UnitOfMeasure unitOfMeasure,
        Category category,
        InventoryType inventoryType,
        int? barcode,
        decimal? packSize,
        string handlingNote,
        bool qiCheck,
        string notes,
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
            FilterTypeName = filterType.Name,
            UnitOfMeasureName = unitOfMeasure.Name,
            CategoryId = category.Id,
            InventoryTypeName = inventoryType.Name,
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
            Active = true
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
        Guid categoryId,
        string inventoryTypeName,
        int? barcode,
        decimal? packSize,
        string handlingNotes,
        bool qiCheck,
        string notes,
        decimal? reorderLevel,
        decimal? height,
        decimal? width,
        decimal? length,
        bool weightInput,
        bool active)
    {

        Sku = sku;
        Name = name;
        ParentId = parentId;
        FilterTypeName = filterTypeName;
        UnitOfMeasureName = unitOfMeasureName;
        CategoryId = categoryId;
        InventoryTypeName = inventoryTypeName;
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
        Active = active;

        Raise(new ProductUpdatedDomainEvent(
            Sku,
            Name,
            ParentId,
            FilterTypeName,
            UnitOfMeasureName,
            CategoryId,
            InventoryTypeName,
            Barcode,
            PackSize,
            HandlingNotes,
            QiCheck,
            Notes,
            ReorderLevel,
            Height,
            Width,
            Length,
            WeightInput,
            Active));
    }

}
