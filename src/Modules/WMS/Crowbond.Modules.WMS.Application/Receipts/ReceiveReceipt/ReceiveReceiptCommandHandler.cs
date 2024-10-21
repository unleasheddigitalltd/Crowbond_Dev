using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Receipts;
using Crowbond.Modules.WMS.Domain.Sequences;
using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Receipts.ReceiveReceipt;

internal sealed class ReceiveReceiptCommandHandler(
    IReceiptRepository receiptRepository,
    ITaskRepository taskRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ReceiveReceiptCommand>
{
    public async Task<Result> Handle(ReceiveReceiptCommand request, CancellationToken cancellationToken)
    {
        ReceiptHeader? receipt = await receiptRepository.GetAsync(request.ReceiptId, cancellationToken);

        if (receipt is null)
        {
            return Result.Failure(ReceiptErrors.NotFound(request.ReceiptId));
        }

        Result receiptResult = receipt.Receive(dateTimeProvider.UtcNow);

        if (receiptResult.IsFailure)
        {
            return Result.Failure(receiptResult.Error);
        }

        Sequence? sequence = await taskRepository.GetSequenceAsync(cancellationToken);
        if (sequence is null)
        {
            return Result.Failure(TaskErrors.SequenceNotFound());
        }

        Result<TaskHeader> taskResult = TaskHeader.Create(
            sequence.GetNumber(),
            receipt.Id,
            null,
            TaskType.putaway);

        if (taskResult.IsFailure)
        {
            return Result.Failure(taskResult.Error);
        }

        taskRepository.Insert(taskResult.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
