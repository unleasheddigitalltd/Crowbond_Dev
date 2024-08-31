using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.StartPutAwayTask;
internal sealed class StartPutAwayTaskCommandValidator : AbstractValidator<StartPutAwayTaskCommand>
{
    public StartPutAwayTaskCommandValidator()
    {
        RuleFor(t => t.TaskHeaderId).NotEmpty();
    }
}
