using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Orders.AdjustOrderLine;

internal sealed class AdjustOrderLineCommandValidator : AbstractValidator<AdjustOrderLineCommand>
{
    public AdjustOrderLineCommandValidator()
    {
        RuleFor(o => o.OrderLineId).NotEmpty();
    }
}
