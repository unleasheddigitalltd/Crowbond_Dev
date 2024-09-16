using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Carts.RemoveItemFromCart;

internal sealed class RemoveItemFromCartCommandValidator : AbstractValidator<RemoveItemFromCartCommand>
{
    public RemoveItemFromCartCommandValidator()
    {
        RuleFor(c => c.ContactId).NotEmpty();
        RuleFor(c => c.ProductId).NotEmpty();
    }
}
