using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Dispatches;
using Crowbond.Modules.WMS.Domain.Receipts;
using Crowbond.Modules.WMS.Domain.Sequences;
using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Dispatches.CreateDispatch;
internal sealed class CreateDispatchCommandHandler(
    IDispatchRepository dispatchRepository,
    ITaskRepository taskRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateDispatchCommand>
{
    public async Task<Result> Handle(CreateDispatchCommand request, CancellationToken cancellationToken)
    {
        Sequence? dispatchSeq = await dispatchRepository.GetSequenceAsync(cancellationToken);

        if (dispatchSeq is null)
        {
            return Result.Failure<Guid>(ReceiptErrors.SequenceNotFound());
        }

        Result<DispatchHeader> dispatchResult = DispatchHeader.Create(
            dispatchSeq.GetNumber(),
            request.Dispatch.OrderId,
        request.Dispatch.OrderNo);

        foreach (DispatchLineRequest line in request.Dispatch.Lines)
        {
            dispatchResult.Value.AddLine(
                line.ProductId,
                line.QuantityReceived);
        }

        dispatchRepository.Insert(dispatchResult.Value);

        // generate the picking task
        Sequence? taskSeq = await taskRepository.GetSequenceAsync(cancellationToken);
        if (taskSeq is null)
        {
            return Result.Failure(TaskErrors.SequenceNotFound());
        }

        Result<TaskHeader> pickingTaskResult = TaskHeader.Create(
            taskSeq.GetNumber(),
            null,
            dispatchResult.Value.Id,
            TaskType.picking);

        if (pickingTaskResult.IsFailure)
        {
            return Result.Failure(pickingTaskResult.Error);
        }

        taskRepository.Insert(pickingTaskResult.Value);

        // generate the packing task
        Result<TaskHeader> packingTskResult = TaskHeader.Create(
            taskSeq.GetNumber(),
            null,
            dispatchResult.Value.Id,
            TaskType.packing);

        if (packingTskResult.IsFailure)
        {
            return Result.Failure(packingTskResult.Error);
        }

        taskRepository.Insert(packingTskResult.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
