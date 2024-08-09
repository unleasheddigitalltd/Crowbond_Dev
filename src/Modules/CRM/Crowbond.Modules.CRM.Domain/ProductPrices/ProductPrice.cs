using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.ProductPrices;

public sealed class ProductPrice : Entity
{
    public ProductPrice()
    {        
    }

    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public string ProductSku { get; private set; }
    public string UnitOfMeasureName { get; private set; }
    public Guid CategoryId { get; private set; }
    public Guid PriceTierId { get; private set; }
    public decimal BasePurchasePrice { get; private set; }
    public decimal SalePrice { get; private set; }
    public DateOnly EffectiveDate { get; private set; }

    public static ProductPrice Create(
        Guid productId, 
        string productName, 
        string productSku, 
        string unitOfMeasureName, 
        Guid categoryId,
        Guid priceTierId,
        decimal basePurchasePrice,
        decimal salePrice,
        DateOnly effectiveDate)
    {
        var productPrice = new ProductPrice
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            ProductName = productName,
            ProductSku = productSku,
            UnitOfMeasureName = unitOfMeasureName,
            CategoryId = categoryId,
            PriceTierId = priceTierId,
            BasePurchasePrice = basePurchasePrice,
            SalePrice = salePrice,
            EffectiveDate = effectiveDate
        };

        return productPrice;
    }

    public void Update(
        decimal basePurchasePrice,
        decimal salePrice,
        DateOnly effectiveDate)
    {
        BasePurchasePrice = basePurchasePrice;
        SalePrice = salePrice;
        EffectiveDate = effectiveDate;
    }
}
