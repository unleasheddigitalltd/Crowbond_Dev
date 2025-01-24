using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Receipts;
using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.CompletePutAwayTask;

internal sealed class CompletePutAwayReceiptLineCommandHandler(
    ITaskRepository taskRepository,
    IReceiptRepository receiptRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CompletePutAwayReceiptLineCommand>
{
    public async Task<Result> Handle(CompletePutAwayReceiptLineCommand request, CancellationToken cancellationToken)
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

        ReceiptHeader? receiptHeader = await receiptRepository.GetAsync(taskHeader.ReceiptId ?? Guid.Empty, cancellationToken);
        
        if (receiptHeader is null)
        {
            return Result.Failure(ReceiptErrors.NotFound(taskHeader.ReceiptId ?? Guid.Empty));
        }

        Result result = receiptHeader.FinalizeLineStorage(request.ReceiptLineId);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        // Check if all receipt lines are stored the Complete the task.
        if (receiptHeader.Lines.All(l => l.IsStored))
        {
            taskHeader.Complete(dateTimeProvider.UtcNow);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result;
    }
}
