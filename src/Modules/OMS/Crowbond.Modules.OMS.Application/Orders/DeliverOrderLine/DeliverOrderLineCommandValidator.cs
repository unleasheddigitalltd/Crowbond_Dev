using FluentValidation;

namespace Crowbond.Modules.OMS.Application.Orders.DeliverOrderLine;

internal sealed class DeliverOrderLineCommandValidator : AbstractValidator<DeliverOrderLineCommand>
{
    public DeliverOrderLineCommandValidator()
    {
        RuleFor(ol => ol.OrderLineId).NotEmpty();
        RuleFor(ol => ol.DriverId).NotEmpty();
        RuleFor(ol => ol.OrderLine.DeliveredQty).NotEmpty().GreaterThanOrEqualTo(decimal.Zero);
        RuleForEach(ol => ol.OrderLine.Rejects)
            .ChildRules(reject =>
            {
                reject.RuleFor(r => r.RejectQty).NotEmpty().GreaterThanOrEqualTo(decimal.Zero);
                reject.RuleFor(r => r.RejectReasonId).NotEmpty();
                reject.RuleFor(r => r.Comments).MaximumLength(255);
            });
    }
}
