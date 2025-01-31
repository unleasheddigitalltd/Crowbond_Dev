using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Dispatches;
using Crowbond.Modules.WMS.Domain.Receipts;
using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.CompletePickingDispatchLine;

internal sealed class CompletePickingDispatchLineCommandHandler(
    ITaskRepository taskRepository,
    IDispatchRepository dispatchRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CompletePickingDispatchLineCommand>
{
    public async Task<Result> Handle(CompletePickingDispatchLineCommand request, CancellationToken cancellationToken)
    {
        TaskHeader? taskHeader = await taskRepository.GetAsync(request.TaskHeaderId, cancellationToken);

        if (taskHeader is null)
        {
            return Result.Failure(TaskErrors.NotFound(request.TaskHeaderId));
        }

        if (!taskHeader.IsAssignedTo(request.UserId))
        {
            return Result.Failure(TaskErrors.ActiveAssignmentForOperatorNotFound(request.UserId));
        }

        DispatchHeader? dispatch = await dispatchRepository.GetAsync(taskHeader.DispatchId ?? Guid.Empty, cancellationToken);

        if (dispatch is null)
        {
            return Result.Failure(ReceiptErrors.NotFound(taskHeader.DispatchId ?? Guid.Empty));
        }

        Result result = dispatch.FinalizeLinePicking(request.DispatchLineId);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        // Check if all dispatch lines are picked the Complete the task.
        if (dispatch.Lines.All(l => l.IsPicked))
        {
            taskHeader.Complete(dateTimeProvider.UtcNow);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result;
    }
}
