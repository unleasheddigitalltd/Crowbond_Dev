using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.UnpausePickingTask;

internal sealed class UnpausePickingTaskCommandValidator : AbstractValidator<UnpausePickingTaskCommand>
{
    public UnpausePickingTaskCommandValidator()
    {
        RuleFor(t => t.TaskHeaderId).NotEmpty();
    }
}
