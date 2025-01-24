using FluentValidation;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.CreatePurchaseOrder;

internal sealed class CreatePurchaseOrderCommandValidator : AbstractValidator<CreatePurchaseOrderCommand>
{
    public CreatePurchaseOrderCommandValidator()
    {
        RuleFor(po => po.PurchaseOrder.SupplierId).NotEmpty();
        RuleFor(po => po.PurchaseOrder.RequiredDate).NotEmpty();
        RuleFor(po => po.PurchaseOrder.PurchaseOrderNotes).MaximumLength(500);
    }
}
