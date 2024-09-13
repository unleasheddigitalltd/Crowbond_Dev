using FluentValidation;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.UpdateCustomerProduct;

internal sealed class UpdateCustomerProductCommandValidator : AbstractValidator<UpdateCustomerProductCommand>
{
    public UpdateCustomerProductCommandValidator()
    {
        RuleFor(s => s.CustomerId).NotEmpty();

        RuleForEach(s => s.CustomerProducts).ChildRules(products =>
        {
            products.RuleFor(p => p.ProductId).NotEmpty();
            products.RuleFor(p => p.FixedPrice).GreaterThan(0);
            products.RuleFor(p => p.FixedDiscount).GreaterThan(0).LessThanOrEqualTo(100);
            products.RuleFor(p => p.Comments).MaximumLength(255);
        });

        RuleFor(p => p.CustomerProducts).Must(coll => coll.GroupBy(p => p.ProductId).Count() == coll.Count)
            .WithMessage(coll => $"One or more items have duplicate products");
    }
}
