using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Tasks.Checking.QuitCheckingTask;

internal sealed class QuitCheckingTaskCommandValidator : AbstractValidator<QuitCheckingTaskCommand>
{
    public QuitCheckingTaskCommandValidator()
    {
        RuleFor(t => t.TaskHeaderId).NotEmpty();
    }
}
