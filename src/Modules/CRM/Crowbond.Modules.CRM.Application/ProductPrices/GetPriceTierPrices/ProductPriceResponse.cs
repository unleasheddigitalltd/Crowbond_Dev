namespace Crowbond.Modules.CRM.Application.ProductPrices.GetPriceTierPrices;

public sealed record ProductPriceResponse
{
    public Guid Id { get; }
    public Guid ProductId { get; }
    public string ProductName { get; }
    public string ProductSku { get; }
    public string UnitOfMeasureName { get; }
    public Guid CategoryId { get; }
    public string CategoryName { get; }
    public Guid PriceTierId { get; }
    public decimal BasePurchasePrice { get; }
    public decimal SalePrice { get; }
    public DateOnly EffectiveDate { get; }
}
