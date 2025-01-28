using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.QuitPickingTask;

internal sealed class QuitPickingTaskCommandValidator: AbstractValidator<QuitPickingTaskCommand>
{
    public QuitPickingTaskCommandValidator()
    {
        RuleFor(t => t.TaskHeaderId).NotEmpty();
    }
}
