namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProductPrice;
public sealed record CustomerProductPriceResponse
{
    public Guid Id { get; }
    public Guid CustomerId { get; }
    public Guid ProductId { get; }
    public string ProductName { get; }
    public string ProductSku { get; }
    public string UnitOfMeasureName { get; }
    public Guid CategoryId { get; }
    public string CategoryName { get; }
    public decimal UnitPrice { get; }
    public int TaxRateType { get; }
}
