using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Tasks.Checking.StartCheckingTask;

internal sealed class StartCheckingTaskCommandValidator : AbstractValidator<StartCheckingTaskCommand>
{
    public StartCheckingTaskCommandValidator()
    {
        RuleFor(t => t.TaskHeaderId).NotEmpty();
    }
}
