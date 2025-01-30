using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Tasks.Checking.UpdateCheckingTask;

internal sealed class UpdateCheckingTaskCommandValidator : AbstractValidator<UpdateCheckingTaskCommand>
{
    public UpdateCheckingTaskCommandValidator()
    {
        RuleFor(x => x.TaskHeaderId).NotEmpty();

        RuleFor(x => x.CheckingDispatchLines).NotNull().Must(lines => lines.Any());

        RuleForEach(x => x.CheckingDispatchLines).ChildRules(line =>
        {
            line.RuleFor(l => l.DispatchLineId).NotEmpty();
        });
    }
}
