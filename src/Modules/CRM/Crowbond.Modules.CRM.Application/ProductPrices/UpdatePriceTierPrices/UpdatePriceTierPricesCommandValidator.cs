using FluentValidation;

namespace Crowbond.Modules.CRM.Application.ProductPrices.UpdatePriceTierPrices;
internal sealed class UpdatePriceTierPricesCommandValidator : AbstractValidator<UpdatePriceTierPricesCommand>
{
    public UpdatePriceTierPricesCommandValidator()
    {
        RuleFor(r => r.PriceTierId).NotEmpty();
        RuleForEach(p => p.ProductPrices)
            .ChildRules(prices =>
            {
                prices.RuleFor(p => p.ProductId).NotEmpty();
                prices.RuleFor(p => p.BasePurchasePrice).GreaterThan(0);
                prices.RuleFor(p => p.SalePrice).GreaterThan(0);
            });
        RuleFor(p => p.ProductPrices).Must(coll => coll.GroupBy(p => p.ProductId).Count() == coll.Count)
            .WithMessage(coll => $"One or more items have duplicate products");
    }
}
