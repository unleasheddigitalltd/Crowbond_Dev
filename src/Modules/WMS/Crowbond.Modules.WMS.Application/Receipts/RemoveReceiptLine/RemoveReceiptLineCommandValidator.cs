using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Receipts.RemoveReceiptLine;

internal sealed class RemoveReceiptLineCommandValidator : AbstractValidator<RemoveReceiptLineCommand>
{
    public RemoveReceiptLineCommandValidator()
    {
        RuleFor(r => r.ReceiptLineId).NotEmpty();
    }
}
