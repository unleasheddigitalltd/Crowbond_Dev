using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Receipts;
using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.AssignPutAwayTask;

internal sealed class AssignPutAwayTaskCommandHandler(
    ITaskRepository taskRepository,
    IReceiptRepository receiptRepository,
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

        ReceiptHeader? receipt = await receiptRepository.GetAsync(taskHeader.ReceiptId ?? Guid.Empty, cancellationToken);

        if (receipt is null)
        {
            return Result.Failure<Guid>(ReceiptErrors.NotFound(taskHeader.ReceiptId ?? Guid.Empty));            
        }

        if (!receipt.Lines.Any())
        {
            return Result.Failure<Guid>(ReceiptErrors.HasNoLines(taskHeader.ReceiptId ?? Guid.Empty));
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
