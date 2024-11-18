using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.CreateCustomerProduct;

public sealed record CreateCustomerProductCommand(
    Guid CustomerId,
    Guid ProductId,
    decimal? FixedPrice,
    decimal? FixedDiscount,
    string? Comments,
    DateOnly? EffectiveDate,
    DateOnly? ExpiryDate) : ICommand;
