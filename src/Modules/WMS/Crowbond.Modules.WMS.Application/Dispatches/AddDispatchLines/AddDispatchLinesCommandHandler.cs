using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Dispatches;
using Crowbond.Modules.WMS.Domain.Products;
using Crowbond.Modules.WMS.Domain.Sequences;
using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Dispatches.AddDispatchLines;

internal sealed class AddDispatchLinesCommandHandler(
    IDispatchRepository dispatchRepository,
    IProductRepository productRepository,
    ITaskRepository taskRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddDispatchLinesCommand>
{
    public async Task<Result> Handle(AddDispatchLinesCommand request, CancellationToken cancellationToken)
    {
        DispatchHeader? dispatchHeader = await dispatchRepository.GetByRouteTripAsync(request.RouteTripId, cancellationToken);

        if (dispatchHeader is null)
        {
            return Result.Failure(DispatchErrors.ForRouteTripNotFound(request.RouteTripId));
        }

        IEnumerable<TaskHeader> tasks = await taskRepository.GetByDispatchHeader(dispatchHeader.Id, cancellationToken);

        Sequence? taskSeq = await taskRepository.GetSequenceAsync(cancellationToken);

        if (taskSeq is null)
        {
            return Result.Failure(TaskErrors.SequenceNotFound());
        }

        foreach (DispatchLineRequest line in request.DispatchLines)
        {
            Product? product = await productRepository.GetAsync(line.ProductId, cancellationToken);

            if (product is null)
            {
                return Result.Failure(ProductErrors.NotFound(line.ProductId));
            }

            DispatchLine dispatchLine = dispatchHeader.AddLine(
                line.OrderId,
                line.OrderNo,
                line.CustomerBusinessName,
                line.OrderLineId,
                line.ProductId,
                line.Qty);

            dispatchRepository.AddLine(dispatchLine);

            if (!tasks.Any(t => t.TaskType == TaskType.PickingItem) && product.UnitOfMeasureName != UnitOfMeasure.Kg.Name)
            {
                // generate the picking item task          
                Result<TaskHeader> pickingItemTaskResult = TaskHeader.Create(
                    taskSeq.GetNumber(),
                    null,
                    dispatchHeader.Id,
                    TaskType.PickingItem);

                if (pickingItemTaskResult.IsFailure)
                {
                    return Result.Failure(pickingItemTaskResult.Error);
                }

                taskRepository.Insert(pickingItemTaskResult.Value);
            }

            if (!tasks.Any(t => t.TaskType == TaskType.PickingBulk) && product.UnitOfMeasureName == UnitOfMeasure.Kg.Name)
            {
                // generate the picking bulk task
                Result<TaskHeader> pickingBulkTaskResult = TaskHeader.Create(
                taskSeq.GetNumber(),
                null,
                dispatchHeader.Id,
                TaskType.PickingBulk);

                if (pickingBulkTaskResult.IsFailure)
                {
                    return Result.Failure(pickingBulkTaskResult.Error);
                }

                taskRepository.Insert(pickingBulkTaskResult.Value);
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
