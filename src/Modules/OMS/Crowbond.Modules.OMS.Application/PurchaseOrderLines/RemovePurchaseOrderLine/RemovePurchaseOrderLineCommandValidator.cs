using FluentValidation;

namespace Crowbond.Modules.OMS.Application.PurchaseOrderLines.RemovePurchaseOrderLine;

internal sealed class RemovePurchaseOrderLineCommandValidator : AbstractValidator<RemovePurchaseOrderLineCommand>
{
    public RemovePurchaseOrderLineCommandValidator()
    {
        RuleFor(pl => pl.PurchaseOrderLineId).NotEmpty();
    }
}
