using FluentValidation;

namespace Crowbond.Modules.CRM.Application.ProductPrices.UpdateProductPrices;

internal sealed class UpdateProductPricesCommandValidator : AbstractValidator<UpdateProductPricesCommand>
{
    public UpdateProductPricesCommandValidator()
    {
        RuleFor(r => r.ProductId).NotEmpty();
        RuleForEach(p => p.ProductPrices)
            .ChildRules(prices =>
            {
                prices.RuleFor(p => p.PriceTierId).NotEmpty();
                prices.RuleFor(p => p.BasePurchasePrice).GreaterThan(0);
                prices.RuleFor(p => p.SalePrice).GreaterThan(0);
            });
        RuleFor(p => p.ProductPrices).Must(coll => coll.GroupBy(p => p.PriceTierId).Count() == coll.Count)
            .WithMessage(coll => $"One or more items have duplicate price-tiers");
    }
}
