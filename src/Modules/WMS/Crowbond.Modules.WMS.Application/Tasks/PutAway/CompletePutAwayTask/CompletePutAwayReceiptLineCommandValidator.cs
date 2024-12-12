using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.CompletePutAwayTask;

internal sealed class CompletePutAwayReceiptLineCommandValidator : AbstractValidator<CompletePutAwayReceiptLineCommand>
{
    public CompletePutAwayReceiptLineCommandValidator()
    {
        RuleFor(t => t.ReceiptLineId).NotEmpty();
    }
}
