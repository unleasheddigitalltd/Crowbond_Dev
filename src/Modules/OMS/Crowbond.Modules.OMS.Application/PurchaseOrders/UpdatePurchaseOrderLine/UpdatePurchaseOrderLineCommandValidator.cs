using FluentValidation;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.UpdatePurchaseOrderLine;

internal sealed class UpdatePurchaseOrderLineCommandValidator : AbstractValidator<UpdatePurchaseOrderLineCommand>
{
    public UpdatePurchaseOrderLineCommandValidator()
    {
        RuleFor(ol => ol.PurchaseOrderLineId).NotEmpty();
        RuleFor(ol => ol.Qty).GreaterThan(0);
        RuleFor(ol => ol.UnitPrice).GreaterThan(0);
        RuleFor(ol => ol.Comments).MaximumLength(255);
    }
}
