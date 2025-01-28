using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.CompletePickingDispatchLine;

internal sealed class CompletePickingDispatchLineCommandValidator : AbstractValidator<CompletePickingDispatchLineCommand>
{
    public CompletePickingDispatchLineCommandValidator()
    {
        RuleFor(t => t.DispatchLineId).NotEmpty();
    }
}
