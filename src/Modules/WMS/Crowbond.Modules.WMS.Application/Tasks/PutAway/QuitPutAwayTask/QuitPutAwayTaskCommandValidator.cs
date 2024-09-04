using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.QuitPutAwayTask;

internal sealed class QuitPutAwayTaskCommandValidator : AbstractValidator<QuitPutAwayTaskCommand>
{
    public QuitPutAwayTaskCommandValidator()
    {
        RuleFor(t => t.TaskHeaderId).NotEmpty();
    }
}
