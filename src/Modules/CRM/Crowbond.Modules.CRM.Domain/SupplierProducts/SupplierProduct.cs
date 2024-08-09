using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.SupplierProducts;

public sealed class SupplierProduct : Entity
{
    private SupplierProduct()
    {        
    }

    public Guid Id { get; private set; }

    public Guid SupplierId { get; private set; }

    public Guid ProductId { get; private set; }

    public string ProductName { get; private set; }

    public string ProductSku { get; private set; }

    public string UnitOfMeasureName { get; private set; }

    public Guid CategoryId { get; private set; }

    public decimal UnitPrice { get; private set; }

    public TaxRateType TaxRateType { get; private set; }

    public bool IsDefault { get; private set; }

    public string? Comments {  get; private set; }
    
    public static SupplierProduct Create(
        Guid supplierId,
        Guid productId,
        string productName,
        string productSku,
        string unitOfMeasureName,
        Guid categoryId,
        decimal unitPrice,
        TaxRateType taxRateType,
        bool isDefault,
        string? comments)
    {
        var supplierProduct = new SupplierProduct
        {
            Id = Guid.NewGuid(),
            SupplierId = supplierId,
            ProductId = productId,
            ProductName = productName,
            ProductSku = productSku,
            UnitOfMeasureName = unitOfMeasureName,
            CategoryId = categoryId,
            UnitPrice = unitPrice,
            TaxRateType = taxRateType,
            IsDefault = isDefault,
            Comments = comments
        };

        return supplierProduct;
    }

    public void Update(
        decimal unitPrice,
        TaxRateType taxRateType,
        bool isDefault,
        string? comments)
    {
        UnitPrice = unitPrice;
        TaxRateType = taxRateType;
        IsDefault = isDefault;
        Comments = comments;
    }
}
