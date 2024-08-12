using FluentValidation;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.CreatePurchaseOrder;

internal sealed class CreatePurchaseOrderCommandValidator : AbstractValidator<CreatePurchaseOrderCommand>
{
    public CreatePurchaseOrderCommandValidator()
    {
        RuleFor(po => po.PurchaseOrder.SupplierId).NotEmpty();
        RuleFor(po => po.PurchaseOrder.RequiredDate).NotEmpty();
        RuleFor(po => po.PurchaseOrder.PurchaseOrderNotes).MaximumLength(500);
        RuleForEach(po => po.PurchaseOrder.PurchaseOrderLines)
            .ChildRules(l =>
            {
                l.RuleFor(l => l.Qty).GreaterThan(decimal.Zero);
                l.RuleFor(l => l.ProductId).NotEmpty();
                l.RuleFor(l => l.Comments).MaximumLength(255);
            });
    }
}
