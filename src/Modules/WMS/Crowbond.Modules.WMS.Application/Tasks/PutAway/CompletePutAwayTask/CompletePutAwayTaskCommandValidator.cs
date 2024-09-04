using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.CompletePutAwayTask;

internal sealed class CompletePutAwayTaskCommandValidator : AbstractValidator<CompletePutAwayTaskCommand>
{
    public CompletePutAwayTaskCommandValidator()
    {
        RuleFor(t => t.TaskHeaderId).NotEmpty();
    }
}
