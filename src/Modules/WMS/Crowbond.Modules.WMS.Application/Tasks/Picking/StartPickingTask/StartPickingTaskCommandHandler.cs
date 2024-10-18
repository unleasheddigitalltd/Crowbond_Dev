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
    IDateTimeProvider dateTimeProvider,
    IDispatchRepository dispatchRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<StartPickingTaskCommand>
{
    public async Task<Result> Handle(StartPickingTaskCommand request, CancellationToken cancellationToken)
    {
        TaskHeader? taskHeader = await taskRepository.GetAsync(request.TaskHeaderId, cancellationToken);

        if (taskHeader is null)
        {
            return Result.Failure<Guid>(TaskErrors.NotFound(request.TaskHeaderId));
        }

        DispatchHeader? dispatch = await dispatchRepository.GetAsync(taskHeader.DispatchId ?? Guid.Empty, cancellationToken);

        if (dispatch is null)
        {
            return Result.Failure<Guid>(ReceiptErrors.NotFound(taskHeader.ReceiptId ?? Guid.Empty));
        }

        if (!dispatch.Lines.Any())
        {
            return Result.Failure<Guid>(ReceiptErrors.HasNoLines(taskHeader.ReceiptId ?? Guid.Empty));
        }

        // Prepare a list of product lines from dispatch lines
        var productLines = dispatch.Lines.Select(line => (
            productId: line.ProductId,
            requestedQty: line.Qty
        )).ToList();

        Result<TaskAssignment> assignmentResult = taskHeader.AddAssignmentWithLines(
            request.UserId,
            productLines);

        if (assignmentResult.IsFailure)
        {
            return Result.Failure<Guid>(assignmentResult.Error);
        }

        taskRepository.AddAssignment(assignmentResult.Value);             

        if (!taskHeader.IsAssignedTo(request.UserId))
        {
            return Result.Failure(TaskErrors.ActiveAssignmentForOperatorNotFound(request.UserId));
        }

        Result result = taskHeader.Start(dateTimeProvider.UtcNow);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result;
    }
}
