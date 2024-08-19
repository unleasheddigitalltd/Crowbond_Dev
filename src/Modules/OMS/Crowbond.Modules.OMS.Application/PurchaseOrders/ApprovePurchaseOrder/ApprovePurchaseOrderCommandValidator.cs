using FluentValidation;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.ApprovePurchaseOrder;

internal sealed class ApprovePurchaseOrderCommandValidator : AbstractValidator<ApprovePurchaseOrderCommand>
{
    public ApprovePurchaseOrderCommandValidator()
    {
        RuleFor(po => po.PurchaseOrderHeaderId).NotEmpty();
    }
}
