using System;
using FluentValidation;
namespace Crowbond.Modules.OMS.Application.PurchaseOrderLines.CreatePurchaseOrderLine;

internal sealed class CreatePurchaseOrderLineCommandValidator : AbstractValidator<CreatePurchaseOrderLineCommand>
{
    public CreatePurchaseOrderLineCommandValidator()
    {
        RuleFor(pl => pl.ProductId).NotEmpty();
        RuleFor(pl => pl.PurchaseOrderHeaderId).NotEmpty();
        RuleFor(pl => pl.Qty).GreaterThan(decimal.Zero);
        RuleFor(pl => pl.Comments).MaximumLength(255);
    }
}
