using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.StartPickingTask;

internal sealed class StartPickingTaskCommandValidator: AbstractValidator<StartPickingTaskCommand>
{
    public StartPickingTaskCommandValidator()
    {
        RuleFor(t => t.TaskHeaderId).NotEmpty();
    }
}
