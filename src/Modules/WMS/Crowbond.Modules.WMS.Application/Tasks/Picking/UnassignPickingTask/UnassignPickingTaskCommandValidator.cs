using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.UnassignPickingTask;

internal sealed class UnassignPickingTaskCommandValidator : AbstractValidator<UnassignPickingTaskCommand>
{
    public UnassignPickingTaskCommandValidator()
    {
        RuleFor(t => t.TaskHeaderId).NotEmpty();
    }
}
