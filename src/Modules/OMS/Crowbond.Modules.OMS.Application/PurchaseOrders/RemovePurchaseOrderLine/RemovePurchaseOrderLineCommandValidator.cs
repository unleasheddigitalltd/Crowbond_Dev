using FluentValidation;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.RemovePurchaseOrderLine;

internal sealed class RemovePurchaseOrderLineCommandValidator : AbstractValidator<RemovePurchaseOrderLineCommand>
{
    public RemovePurchaseOrderLineCommandValidator()
    {
        RuleFor(p => p.PurchaseOrderLineId).NotEmpty();
    }
}
