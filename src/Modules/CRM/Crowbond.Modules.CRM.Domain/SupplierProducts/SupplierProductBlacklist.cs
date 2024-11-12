using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.SupplierProducts;

public sealed class SupplierProductBlacklist : Entity , ISoftDeletable, IAuditable
{
    private SupplierProductBlacklist()
    {        
    }

    public Guid Id { get; private set; }

    public Guid SupplierId { get; private set; }

    public Guid ProductId { get; private set; }

    public string? Comments {  get; private set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOnUtc { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOnUtc { get ; set; }

    public static SupplierProductBlacklist Create(
        Guid supplierId,
        Guid productId,
        string? comments)
    {
        var supplierProduct = new SupplierProductBlacklist
        {
            Id = Guid.NewGuid(),
            SupplierId = supplierId,
            ProductId = productId,
            Comments = comments,
        };

        return supplierProduct;
    }

    public void Update(string? comments)
    {
        Comments = comments;
    }
}
