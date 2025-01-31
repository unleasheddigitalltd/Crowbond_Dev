using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Dispatches;
using Crowbond.Modules.WMS.Domain.Receipts;
using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.StartPickingTask;

internal sealed class StartPickingTaskCommandHandler(
    ITaskRepository taskRepository,
    IDispatchRepository dispatchRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<StartPickingTaskCommand>
{
    public async Task<Result> Handle(StartPickingTaskCommand request, CancellationToken cancellationToken)
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

        Result result = taskHeader.Start(dateTimeProvider.UtcNow);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        DispatchHeader? dispatchHeader = await dispatchRepository.GetAsync(taskHeader.DispatchId ?? Guid.Empty, cancellationToken);

        if (dispatchHeader is null)
        {
            return Result.Failure(DispatchErrors.NotFound(taskHeader.DispatchId ?? Guid.Empty));
        }

        Result dispatchResult = dispatchHeader.StartProcessing();

        if (dispatchResult.IsFailure)
        {
            return Result.Failure(dispatchResult.Error);            
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result;
    }
}
