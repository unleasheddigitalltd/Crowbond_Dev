using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.CustomerProducts;

public static class CustomerProductErrors
{
    public static readonly Error EffectiveDateInThePast = Error.Problem(
        "CustomerProducts.EffectiveDateInThePast",
        "The effective date cannot be in the past");

    public static readonly Error ExpiryDateInThePastOrToday = Error.Problem(
        "CustomerProducts.ExpiryDateInThePastOrToday",
        "The expiry date must be in the future");

    public static readonly Error ExpiryDateBeforeEffectiveDate = Error.Problem(
        "CustomerProducts.ExpiryDateBeforeEffectiveDate",
        "The expiry date must be after the effective date");
}
