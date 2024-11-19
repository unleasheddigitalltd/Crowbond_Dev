using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Orders.UpdateOrderLine;

internal sealed class UpdateOrderLineCommandValidator: AbstractValidator<UpdateOrderLineCommand>
{
    public UpdateOrderLineCommandValidator()
    {
        RuleFor(l => l.OrderLineId).NotEmpty();
        RuleFor(l => l.Qty).NotEmpty().GreaterThan(decimal.Zero);
    }
}
