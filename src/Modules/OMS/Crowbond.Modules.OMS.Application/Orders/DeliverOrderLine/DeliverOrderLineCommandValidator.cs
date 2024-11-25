using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Orders.DeliverOrderLine;

internal sealed class DeliverOrderLineCommandValidator: AbstractValidator<DeliverOrderLineCommand>
{
    public DeliverOrderLineCommandValidator()
    {
        RuleFor(ol => ol.OrderLineId).NotEmpty();
        RuleFor(ol => ol.DriverId).NotEmpty();
        RuleFor(ol => ol.DeliveredQty).NotEmpty().GreaterThanOrEqualTo(decimal.Zero);
        RuleFor(ol => ol.DeliveryComments).MaximumLength(255);
    }
}
