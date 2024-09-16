namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProducts;
public sealed record CustomerProductResponse
{
    public Guid Id { get; }
    public Guid CustomerId { get; }
    public Guid ProductId { get; }
    public string ProductName { get; }
    public string ProductSku { get; }
    public string UnitOfMeasureName { get; }
    public Guid CategoryId { get; }
    public string CategoryName { get; }
    public decimal? FixedPrice { get; }
    public decimal? FixedDiscount { get; }
    public string? Comments { get; }
    public DateOnly EffectiveDate { get; }
    public DateOnly? ExpiryDate { get; }
}
