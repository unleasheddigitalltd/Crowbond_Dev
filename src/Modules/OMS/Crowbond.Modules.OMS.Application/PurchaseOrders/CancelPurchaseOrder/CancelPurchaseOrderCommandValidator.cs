using FluentValidation;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.CancelPurchaseOrder;

internal sealed class CancelPurchaseOrderCommandValidator : AbstractValidator<CancelPurchaseOrderCommand>
{
    public CancelPurchaseOrderCommandValidator()
    {
        RuleFor(po => po.PurchaseOrderHeaderId).NotEmpty();
    }
}
