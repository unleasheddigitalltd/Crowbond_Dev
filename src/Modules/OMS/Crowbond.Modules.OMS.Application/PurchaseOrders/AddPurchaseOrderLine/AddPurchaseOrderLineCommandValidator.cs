using FluentValidation;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.AddPurchaseOrderLine;

internal sealed class AddPurchaseOrderLineCommandValidator: AbstractValidator<AddPurchaseOrderLineCommand>
{
    public AddPurchaseOrderLineCommandValidator()
    {
        RuleFor(ol => ol.ProductId).NotEmpty();
        RuleFor(ol => ol.Qty).GreaterThan(0);
        RuleFor(ol => ol.Comments).MaximumLength(255);
    }
}
