using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.ProductPrices;

public sealed class ProductPrice : Entity, ISoftDeletable, IAuditable
{
    private ProductPrice()
    {        
    }

    public Guid Id { get; private set; }

    public Guid ProductId { get; private set; }

    public Guid PriceTierId { get; private set; }

    public decimal BasePurchasePrice { get; private set; }

    public decimal SalePrice { get; private set; }

    public DateOnly EffectiveDate { get; private set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOnUtc { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOnUtc { get; set; }

    public static ProductPrice Create(
        Guid productId,
        Guid priceTierId,
        decimal basePurchasePrice,
        decimal salePrice,
        DateOnly effectiveDate)
    {
        var productPrice = new ProductPrice
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            PriceTierId = priceTierId,
            BasePurchasePrice = basePurchasePrice,
            SalePrice = salePrice,
            EffectiveDate = effectiveDate
        };

        return productPrice;
    }

    public void Update(DateOnly effectiveDate)
    {
        EffectiveDate = effectiveDate;
    }
}
