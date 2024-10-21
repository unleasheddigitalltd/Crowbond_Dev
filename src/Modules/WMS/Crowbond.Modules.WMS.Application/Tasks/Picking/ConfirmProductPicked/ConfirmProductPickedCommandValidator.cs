using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.ConfirmProductPicked;
internal sealed class ConfirmProductPickedCommandValidator : AbstractValidator<ConfirmProductPickedCommand>
{
    public ConfirmProductPickedCommandValidator()
    {
        RuleFor(t => t.TaskAssignmentLineId).NotEmpty();
        RuleFor(t => t.ToLocationId).NotEmpty();
        RuleFor(t => t.StockId).NotEmpty();
    }
}
