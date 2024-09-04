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

        ReceiptHeader? receipt = await receiptRepository.GetAsync(taskHeader.EntityId, cancellationToken);

        if (receipt is null)
        {
            return Result.Failure<Guid>(ReceiptErrors.NotFound(taskHeader.EntityId));            
        }

        if (!receipt.Lines.Any())
        {
            return Result.Failure<Guid>(ReceiptErrors.HasNoLines(taskHeader.EntityId));
        }

        // Prepare a list of product lines from receiptLines
        var productLines = receipt.Lines.Select(line => (
            productId: line.ProductId,
            requestedQty: line.QuantityReceived
        )).ToList();

        Result<TaskAssignment> result = taskHeader.AddAssignmentWithLines(
            warehouseOperatorId: request.WarehouseOperatorId,
            createdBy: request.UserId,
            createdDate: dateTimeProvider.UtcNow,
            productLines: productLines);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        taskRepository.AddAssignment(result.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
