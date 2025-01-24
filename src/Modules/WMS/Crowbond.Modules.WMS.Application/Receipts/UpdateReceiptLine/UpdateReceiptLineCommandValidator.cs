using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Receipts.UpdateReceiptLine;

internal sealed class UpdateReceiptLineCommandValidator : AbstractValidator<UpdateReceiptLineCommand>
{
    public UpdateReceiptLineCommandValidator()
    {
        RuleFor(r => r.ReceiptLineId).NotEmpty();
        RuleFor(r => r.UnitPrice).GreaterThan(decimal.Zero);
        RuleFor(r => r.QuantityReceived).GreaterThan(decimal.Zero);
    }
}
