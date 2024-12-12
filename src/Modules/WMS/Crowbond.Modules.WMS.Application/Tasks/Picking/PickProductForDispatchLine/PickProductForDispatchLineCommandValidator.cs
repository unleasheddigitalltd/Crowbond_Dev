using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.PickProductForDispatchLine;

internal sealed class PickProductForDispatchLineCommandValidator: AbstractValidator<PickProductForDispatchLineCommand>
{
    public PickProductForDispatchLineCommandValidator()
    {
        RuleFor(t => t.TaskHeaderId).NotEmpty();
        RuleFor(t => t.UserId).NotEmpty();
        RuleFor(t => t.DispatchLineId).NotEmpty();
        RuleFor(t => t.Qty).NotEmpty().GreaterThan(decimal.Zero);
    }
}
