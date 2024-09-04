using FluentValidation;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.AssignPutAwayTask;

internal sealed class AssignPutAwayTaskCommandValidator : AbstractValidator<AssignPutAwayTaskCommand>
{
    public AssignPutAwayTaskCommandValidator()
    {
        RuleFor(t => t.WarehouseOperatorId).NotEmpty();
        RuleFor(t => t.TaskHeaderId).NotEmpty();
    }
}
