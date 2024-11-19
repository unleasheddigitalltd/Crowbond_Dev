using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Orders.AddOrderLine;

internal sealed class AddOrderLineCommandValidator: AbstractValidator<AddOrderLineCommand>
{
    public AddOrderLineCommandValidator()
    {
        RuleFor(l => l.OrderHeaderId).NotEmpty();
        RuleFor(l => l.ProductId).NotEmpty();
        RuleFor(l => l.Qty).NotEmpty().GreaterThan(decimal.Zero);
    }
}
