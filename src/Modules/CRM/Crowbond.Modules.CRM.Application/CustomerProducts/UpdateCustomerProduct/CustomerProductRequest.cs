namespace Crowbond.Modules.CRM.Application.CustomerProducts.UpdateCustomerProduct;

public sealed record CustomerProductRequest(
    Guid ProductId,
    decimal? FixedPrice,
    decimal? FixedDiscount,
    string? Comments,
    DateOnly EffectiveDate,
    DateOnly? ExpiryDate);
