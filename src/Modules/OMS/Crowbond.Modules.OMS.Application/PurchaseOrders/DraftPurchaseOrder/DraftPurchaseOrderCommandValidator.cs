using FluentValidation;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.DraftPurchaseOrder;

internal sealed class DraftPurchaseOrderCommandValidator : AbstractValidator<DraftPurchaseOrderCommand>
{
    public DraftPurchaseOrderCommandValidator()
    {
        RuleFor(po => po.PurchaseOrderHeaderId).NotEmpty();
    }
}
