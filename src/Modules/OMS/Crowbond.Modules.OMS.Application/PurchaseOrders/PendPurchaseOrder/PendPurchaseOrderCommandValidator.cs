using System;
using FluentValidation;
namespace Crowbond.Modules.OMS.Application.PurchaseOrders.PendPurchaseOrder;

internal sealed class PendPurchaseOrderCommandValidator : AbstractValidator<PendPurchaseOrderCommand>
{
    public PendPurchaseOrderCommandValidator()
    {
        RuleFor(po => po.PurchaseOrderHeaderId).NotEmpty();
    }
}
