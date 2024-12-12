using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.LocateProductForPutAway;

internal sealed class LocateProductForPutAwayCommandValidator : AbstractValidator<LocateProductForPutAwayCommand>
{
    public LocateProductForPutAwayCommandValidator()
    {
        RuleFor(t => t.TaskHeaderId).NotEmpty();
        RuleFor(t => t.ReceiptLineId).NotEmpty();
        RuleFor(t => t.LocationId).NotEmpty();
        RuleFor(t => t.Qty).GreaterThan(0);
    }
}
