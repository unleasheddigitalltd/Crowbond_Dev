using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.CustomerProducts;

public static class CustomerProductErrors
{
    public static Error NotFound(Guid customerId, Guid productId) =>
    Error.NotFound("CustomerProducts.NotFound", $"The product with the identifier {productId} for customer with the identifier {customerId} was not found");

    public static readonly Error EffectiveDateIsNull = Error.Problem(
        "CustomerProducts.EffectiveDateIsNull",
        "The effective date cannot be null");

    public static readonly Error EffectiveDateInThePast = Error.Problem(
        "CustomerProducts.EffectiveDateInThePast",
        "The effective date cannot be in the past");

    public static readonly Error ExpiryDateInThePastOrToday = Error.Problem(
        "CustomerProducts.ExpiryDateInThePastOrToday",
        "The expiry date must be in the future");

    public static readonly Error ExpiryDateBeforeEffectiveDate = Error.Problem(
        "CustomerProducts.ExpiryDateBeforeEffectiveDate",
        "The expiry date must be after the effective date");

    public static readonly Error FixedDiscountAndFixedPriceConflict = Error.Conflict(
        "CustomerProducts.FixedDiscountAndFixedPriceConflict",
        "Both fixed discount and fixed price cannot be applied simultaneously");

    public static readonly Error EffectiveDateWithoutPricing = Error.Problem(
        "CustomerProducts.EffectiveDateWithoutPricing",
        "An effective date is provided, but neither fixed discount nor fixed price is specified");
}
