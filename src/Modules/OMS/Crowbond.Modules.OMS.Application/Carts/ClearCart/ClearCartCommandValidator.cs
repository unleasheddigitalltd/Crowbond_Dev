using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Carts.ClearCart;

internal sealed class ClearCartCommandValidator : AbstractValidator<ClearCartCommand>
{
    public ClearCartCommandValidator()
    {
        RuleFor(c => c.ContactId).NotEmpty();
    }
}
