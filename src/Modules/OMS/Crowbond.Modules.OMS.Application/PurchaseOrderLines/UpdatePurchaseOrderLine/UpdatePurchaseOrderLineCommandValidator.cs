using FluentValidation;

namespace Crowbond.Modules.OMS.Application.PurchaseOrderLines.UpdatePurchaseOrderLine;

internal sealed class UpdatePurchaseOrderLineCommandValidator : AbstractValidator<UpdatePurchaseOrderLineCommand>
{
    public UpdatePurchaseOrderLineCommandValidator()
    {
        RuleFor(pl => pl.PurchaseOrderLineId).NotEmpty();
        RuleFor(pl => pl.Qty).GreaterThan(decimal.Zero);
        RuleFor(pl => pl.Comments).MaximumLength(255);
    }
}
