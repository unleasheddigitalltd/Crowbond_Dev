using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Tasks.Checking.AssignCheckingTask;

internal sealed class AssignCheckingTaskCommandValidator : AbstractValidator<AssignCheckingTaskCommand>
{
    public AssignCheckingTaskCommandValidator()
    {
        RuleFor(t => t.WarehouseOperatorId).NotEmpty();
        RuleFor(t => t.TaskHeaderId).NotEmpty();
    }
}
