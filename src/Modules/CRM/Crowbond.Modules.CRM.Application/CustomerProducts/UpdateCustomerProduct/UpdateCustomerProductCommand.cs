using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.UpdateCustomerProduct;

public sealed record UpdateCustomerProductCommand(
    Guid CustomerId,
    Guid ProductId,
    decimal? FixedPrice,
    decimal? FixedDiscount,
    string? Comments,
    DateOnly? EffectiveDate,
    DateOnly? ExpiryDate) : ICommand;
