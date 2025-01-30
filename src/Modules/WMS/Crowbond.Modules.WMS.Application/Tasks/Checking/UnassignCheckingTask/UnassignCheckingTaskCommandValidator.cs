using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Tasks.Checking.UnassignCheckingTask;

internal sealed class UnassignCheckingTaskCommandValidator : AbstractValidator<UnassignCheckingTaskCommand>
{
    public UnassignCheckingTaskCommandValidator()
    {
        RuleFor(t => t.TaskHeaderId).NotEmpty();
    }
}
