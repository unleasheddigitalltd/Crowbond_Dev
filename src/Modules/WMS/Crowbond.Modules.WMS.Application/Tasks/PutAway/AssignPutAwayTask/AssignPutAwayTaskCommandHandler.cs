using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Receipts;
using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.AssignPutAwayTask;

internal sealed class AssignPutAwayTaskCommandHandler(
    ITaskRepository taskRepository,
    IReceiptRepository receiptRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AssignPutAwayTaskCommand, Guid>
{
    public async Task<Result<Guid>> Handle(AssignPutAwayTaskCommand request, CancellationToken cancellationToken)
    {
        TaskHeader? taskHeader = await taskRepository.GetAsync(request.TaskHeaderId, cancellationToken);

        if (taskHeader is null)
        {
            return Result.Failure<Guid>(TaskErrors.NotFound(request.TaskHeaderId));
        }

        IEnumerable<ReceiptLine> receiptLines = await receiptRepository.GetLinesAsync(taskHeader.EntityId, cancellationToken);

        if (!receiptLines.Any())
        {
            return Result.Failure<Guid>(ReceiptErrors.HasNoLines(taskHeader.EntityId));
        }

        Result<TaskAssignment> result = taskHeader.AddAssignment(
            request.WarehouseOperatorId,
            request.UserId,
            dateTimeProvider.UtcNow);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        foreach (ReceiptLine receiptLine in receiptLines)
        {
            Result resultLine = taskHeader.AddAssignmentLine(
                productId: receiptLine.ProductId,
                requestedQty: receiptLine.QuantityReceived);

            if (resultLine.IsFailure)
            {
                return Result.Failure<Guid>(resultLine.Error);
            }
        }

        taskRepository.AddAssignment(result.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
