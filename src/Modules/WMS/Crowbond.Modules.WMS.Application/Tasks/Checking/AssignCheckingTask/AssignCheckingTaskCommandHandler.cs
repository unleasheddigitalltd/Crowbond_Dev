using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Dispatches;
using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Tasks.Checking.AssignCheckingTask;

internal sealed class AssignCheckingTaskCommandHandler(
    ITaskRepository taskRepository,
    IDispatchRepository dispatchRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AssignCheckingTaskCommand, Guid>
{
    public async Task<Result<Guid>> Handle(AssignCheckingTaskCommand request, CancellationToken cancellationToken)
    {
        TaskHeader? taskHeader = await taskRepository.GetAsync(request.TaskHeaderId, cancellationToken);

        if (taskHeader is null)
        {
            return Result.Failure<Guid>(TaskErrors.NotFound(request.TaskHeaderId));
        }

        DispatchHeader? dispatch = await dispatchRepository.GetAsync(taskHeader.DispatchId ?? Guid.Empty, cancellationToken);

        if (dispatch is null)
        {
            return Result.Failure<Guid>(DispatchErrors.NotFound(taskHeader.DispatchId ?? Guid.Empty));
        }

        if (!dispatch.Lines.Any())
        {
            return Result.Failure<Guid>(DispatchErrors.HasNoLines(taskHeader.ReceiptId ?? Guid.Empty));
        }


        Result<TaskAssignment> result = taskHeader.AddAssignment(request.WarehouseOperatorId);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        taskRepository.AddAssignment(result.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
