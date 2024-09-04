using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.UnpausePutAwayTask;

internal sealed class UnpausePutAwayTaskCommandValidator : AbstractValidator<UnpausePutAwayTaskCommand>
{
    public UnpausePutAwayTaskCommandValidator()
    {
        RuleFor(t => t.TaskHeaderId).NotEmpty();
    }
}
