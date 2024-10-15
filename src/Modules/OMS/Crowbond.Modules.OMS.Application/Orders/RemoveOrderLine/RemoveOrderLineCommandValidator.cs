using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Orders.RemoveOrderLine;

internal sealed class RemoveOrderLineCommandValidator: AbstractValidator<RemoveOrderLineCommand>
{
    public RemoveOrderLineCommandValidator()
    {
        RuleFor(ol => ol.OrderLineId).NotEmpty();
    }
}
