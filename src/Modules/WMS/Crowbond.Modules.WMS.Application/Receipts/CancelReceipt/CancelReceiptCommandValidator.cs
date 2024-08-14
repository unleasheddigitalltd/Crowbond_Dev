using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Receipts.CancelReceipt;

internal sealed class CancelReceiptCommandValidator : AbstractValidator<CancelReceiptCommand>
{
    public CancelReceiptCommandValidator()
    {
        RuleFor(r => r.PurchaseOrderId).NotEmpty();
        RuleFor(r => r.UtcNow).NotEmpty();
        RuleFor(r => r.UserId).NotEmpty();
    }
}
