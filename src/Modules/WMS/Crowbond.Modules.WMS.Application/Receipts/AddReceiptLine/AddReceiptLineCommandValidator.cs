using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Receipts.AddReceiptLine;

internal sealed class AddReceiptLineCommandValidator : AbstractValidator<AddReceiptLineCommand>
{
    public AddReceiptLineCommandValidator()
    {
        RuleFor(r => r.ReceiptHeaderId).NotEmpty();
        RuleFor(r => r.ProductId).NotEmpty();
        RuleFor(r => r.UnitPrice).GreaterThan(decimal.Zero);
        RuleFor(r => r.QuantityReceived).GreaterThan(decimal.Zero);

    }
}
