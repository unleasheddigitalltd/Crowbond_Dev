using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.AssignPickingTask;

internal sealed class AssignPickingTaskCommandValidator : AbstractValidator<AssignPickingTaskCommand>
{
    public AssignPickingTaskCommandValidator()
    {
        RuleFor(t => t.WarehouseOperatorId).NotEmpty();
        RuleFor(t => t.TaskHeaderId).NotEmpty();
    }
}
