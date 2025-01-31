using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Dispatches;
using Crowbond.Modules.WMS.Domain.Receipts;
using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Tasks.Checking.UpdateCheckingTask;

internal sealed class UpdateCheckingTaskCommandHandler(
    ITaskRepository taskRepository,
    IDispatchRepository dispatchRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCheckingTaskCommand>
{
    public async Task<Result> Handle(UpdateCheckingTaskCommand request, CancellationToken cancellationToken)
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

        // Retrieve dispatch
        DispatchHeader? dispatchHeader = await dispatchRepository.GetAsync(taskHeader.DispatchId ?? Guid.Empty, cancellationToken);

        if (dispatchHeader is null)
        {
            return Result.Failure<Guid>(ReceiptErrors.NotFound(taskHeader.ReceiptId ?? Guid.Empty));
        }

        foreach (CheckingDispatchLineRequest line in request.CheckingDispatchLines)
        {
            Result result = dispatchHeader.CheckLine(line.DispatchLineId, line.IsChecked);
            if (result.IsFailure)
            {
                return result;
            }
        }

        if (dispatchHeader.Status == DispatchStatus.Completed)
        {
            Result result = taskHeader.Complete(dateTimeProvider.UtcNow);
            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
