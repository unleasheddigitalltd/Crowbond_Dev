using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.PausePickingTask;
internal sealed class PausePickingTaskCommandVlidator : AbstractValidator<PausePickingTaskCommand>
{
    public PausePickingTaskCommandVlidator()
    {
        RuleFor(t => t.TaskHeaderId).NotEmpty();
    }
}
