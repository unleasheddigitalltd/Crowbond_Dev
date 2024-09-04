using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.PausePutAwayTask;

internal sealed class PausePutAwayTaskCommandValidator : AbstractValidator<PausePutAwayTaskCommand>
{
    public PausePutAwayTaskCommandValidator()
    {
        RuleFor(t => t.TaskHeaderId).NotEmpty();
    }
}
