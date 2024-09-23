using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Carts.AddItemToCart;

internal sealed class AddItemToCartCommandValidator : AbstractValidator<AddItemToCartCommand>
{
    public AddItemToCartCommandValidator()
    {
        RuleFor(c => c.ContactId).NotEmpty();
        RuleFor(c => c.ProductId).NotEmpty();
        RuleFor(c => c.Qty).GreaterThan(decimal.Zero);
    }
}
