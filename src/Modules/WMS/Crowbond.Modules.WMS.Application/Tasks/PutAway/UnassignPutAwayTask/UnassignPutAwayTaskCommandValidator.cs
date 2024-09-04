using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.UnassignPutAwayTask;

internal sealed class UnassignPutAwayTaskCommandValidator : AbstractValidator<UnassignPutAwayTaskCommand>
{
    public UnassignPutAwayTaskCommandValidator()
    {
        RuleFor(t => t.TaskHeaderId).NotEmpty();
    }
}
