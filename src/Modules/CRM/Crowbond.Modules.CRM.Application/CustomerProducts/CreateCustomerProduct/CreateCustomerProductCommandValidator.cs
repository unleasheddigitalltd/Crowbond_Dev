using FluentValidation;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.CreateCustomerProduct;

internal sealed class CreateCustomerProductCommandValidator : AbstractValidator<CreateCustomerProductCommand>
{
    public CreateCustomerProductCommandValidator()
    {
        RuleFor(cp => cp.CustomerId).NotEmpty();
        RuleFor(cp => cp.ProductId).NotEmpty();
        RuleFor(cp => cp.FixedPrice).GreaterThan(0).PrecisionScale(10, 2, true);
        RuleFor(cp => cp.FixedDiscount).GreaterThan(0).PrecisionScale(5, 2, true);
        RuleFor(cp => cp.Comments).MaximumLength(255);
        RuleFor(cp => cp.ExpiryDate).GreaterThan(cp => cp.EffectiveDate);
        RuleFor(cp => cp.EffectiveDate)
            .NotEmpty()
            .When(cp => cp.FixedPrice > 0 || cp.FixedDiscount > 0)
            .WithMessage("EffectiveDate is required when FixedPrice or FixedDiscount is specified.");
    }
}
