using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.SupplierProducts;

public sealed class SupplierProduct : Entity , ISoftDeletable
{
    private SupplierProduct()
    {        
    }

    public Guid Id { get; private set; }

    public Guid SupplierId { get; private set; }

    public Guid ProductId { get; private set; }

    public decimal UnitPrice { get; private set; }

    public bool IsDefault { get; private set; }

    public string? Comments {  get; private set; }

    public Guid CreatedBy { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public Guid? LastModifiedBy { get; private set; }

    public DateTime? LastModifiedOnUtc { get; private set; }

    public bool IsDeleted { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOnUtc { get ; set; }

    public static SupplierProduct Create(
        Guid supplierId,
        Guid productId,
        decimal unitPrice,
        bool isDefault,
        string? comments)
    {
        var supplierProduct = new SupplierProduct
        {
            Id = Guid.NewGuid(),
            SupplierId = supplierId,
            ProductId = productId,
            UnitPrice = unitPrice,
            IsDefault = isDefault,
            Comments = comments,
        };

        return supplierProduct;
    }

    public void Update(
        bool isDefault,
        string? comments)
    {
        IsDefault = isDefault;
        Comments = comments;
    }
}
