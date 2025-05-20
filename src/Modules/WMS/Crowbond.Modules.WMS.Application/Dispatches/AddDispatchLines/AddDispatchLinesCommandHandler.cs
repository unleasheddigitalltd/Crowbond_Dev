using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Dispatches;
using Crowbond.Modules.WMS.Domain.Products;
using Crowbond.Modules.WMS.Domain.Sequences;
using Crowbond.Modules.WMS.Domain.Tasks;
using Microsoft.Extensions.Logging;

namespace Crowbond.Modules.WMS.Application.Dispatches.AddDispatchLines;

internal sealed class AddDispatchLinesCommandHandler(
    IDispatchRepository dispatchRepository,
    IProductRepository productRepository,
    ITaskRepository taskRepository,
    IUnitOfWork unitOfWork,
    ILogger<AddDispatchLinesCommandHandler> logger)
    : ICommandHandler<AddDispatchLinesCommand>
{
    public async Task<Result> Handle(AddDispatchLinesCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Processing dispatch lines for route trip {RouteTripId}. Lines count: {LineCount}",
            request.RouteTripId,
            request.DispatchLines.Count);

        DispatchHeader? dispatchHeader = await dispatchRepository.GetByRouteTripAsync(request.RouteTripId, cancellationToken);

        if (dispatchHeader is null)
        {
            logger.LogError(
                "Dispatch header not found for route trip {RouteTripId}",
                request.RouteTripId);
            return Result.Failure(DispatchErrors.ForRouteTripNotFound(request.RouteTripId));
        }

        logger.LogInformation(
            "Found dispatch header {DispatchId} for route trip {RouteTripId}",
            dispatchHeader.Id,
            request.RouteTripId);

        IEnumerable<TaskHeader> tasks = await taskRepository.GetByDispatchHeader(dispatchHeader.Id, cancellationToken);

        Sequence? taskSeq = await taskRepository.GetSequenceAsync(cancellationToken);

        if (taskSeq is null)
        {
            logger.LogError("Task sequence not found");
            return Result.Failure(TaskErrors.SequenceNotFound());
        }

        foreach (DispatchLineRequest line in request.DispatchLines)
        {
            logger.LogInformation(
                "Processing dispatch line for order {OrderId}, product {ProductId}, quantity {Quantity}",
                line.OrderId,
                line.ProductId,
                line.Qty);

            Product? product = await productRepository.GetAsync(line.ProductId, cancellationToken);

            if (product is null)
            {
                logger.LogError(
                    "Product {ProductId} not found for dispatch line in order {OrderId}",
                    line.ProductId,
                    line.OrderId);
                return Result.Failure(ProductErrors.NotFound(line.ProductId));
            }

            DispatchLine dispatchLine = dispatchHeader.AddLine(
                line.OrderId,
                line.OrderNo,
                line.CustomerBusinessName,
                line.OrderLineId,
                line.ProductId,
                line.Qty,
                IsProductBulk(product));

            dispatchRepository.AddLine(dispatchLine);

            logger.LogInformation(
                "Created dispatch line {DispatchLineId} for order {OrderId}, product {ProductId}",
                dispatchLine.Id,
                line.OrderId,
                line.ProductId);

            if (!tasks.Any(t => t.TaskType == TaskType.PickingItem) && !dispatchLine.IsBulk)
            {
                logger.LogInformation(
                    "Creating new picking item task for dispatch line {DispatchLineId}, product {ProductId}",
                    dispatchLine.Id,
                    line.ProductId);

                Result<TaskHeader> pickingItemTaskResult = TaskHeader.Create(
                    taskSeq.GetNumber(),
                    null,
                    dispatchHeader.Id,
                    TaskType.PickingItem);

                if (pickingItemTaskResult.IsFailure)
                {
                    logger.LogError(
                        "Failed to create picking item task for dispatch line {DispatchLineId}, product {ProductId}",
                        dispatchLine.Id,
                        line.ProductId);
                    return Result.Failure(pickingItemTaskResult.Error);
                }

                taskRepository.Insert(pickingItemTaskResult.Value);

                logger.LogInformation(
                    "Created picking item task {TaskId} for dispatch line {DispatchLineId}",
                    pickingItemTaskResult.Value.Id,
                    dispatchLine.Id);
            }

            if (!tasks.Any(t => t.TaskType == TaskType.PickingBulk) && dispatchLine.IsBulk)
            {
                logger.LogInformation(
                    "Creating new picking bulk task for dispatch line {DispatchLineId}, product {ProductId}",
                    dispatchLine.Id,
                    line.ProductId);

                Result<TaskHeader> pickingBulkTaskResult = TaskHeader.Create(
                    taskSeq.GetNumber(),
                    null,
                    dispatchHeader.Id,
                    TaskType.PickingBulk);

                if (pickingBulkTaskResult.IsFailure)
                {
                    logger.LogError(
                        "Failed to create picking bulk task for dispatch line {DispatchLineId}, product {ProductId}",
                        dispatchLine.Id,
                        line.ProductId);
                    return Result.Failure(pickingBulkTaskResult.Error);
                }

                taskRepository.Insert(pickingBulkTaskResult.Value);

                logger.LogInformation(
                    "Created picking bulk task {TaskId} for dispatch line {DispatchLineId}",
                    pickingBulkTaskResult.Value.Id,
                    dispatchLine.Id);
            }

            if (!tasks.Any(t => t.TaskType == TaskType.CheckingItem) && !dispatchLine.IsBulk)
            {
                logger.LogInformation(
                    "Creating new checking item task for dispatch line {DispatchLineId}, product {ProductId}",
                    dispatchLine.Id,
                    line.ProductId);

                Result<TaskHeader> checkingItemTaskResult = TaskHeader.Create(
                    taskSeq.GetNumber(),
                    null,
                    dispatchHeader.Id,
                    TaskType.CheckingItem);

                if (checkingItemTaskResult.IsFailure)
                {
                    logger.LogError(
                        "Failed to create checking item task for dispatch line {DispatchLineId}, product {ProductId}",
                        dispatchLine.Id,
                        line.ProductId);
                    return Result.Failure(checkingItemTaskResult.Error);
                }

                taskRepository.Insert(checkingItemTaskResult.Value);

                logger.LogInformation(
                    "Created checking item task {TaskId} for dispatch line {DispatchLineId}",
                    checkingItemTaskResult.Value.Id,
                    dispatchLine.Id);
            }

            if (!tasks.Any(t => t.TaskType == TaskType.CheckingBulk) && dispatchLine.IsBulk)
            {
                logger.LogInformation(
                    "Creating new checking bulk task for dispatch line {DispatchLineId}, product {ProductId}",
                    dispatchLine.Id,
                    line.ProductId);

                Result<TaskHeader> checkingBulkTaskResult = TaskHeader.Create(
                    taskSeq.GetNumber(),
                    null,
                    dispatchHeader.Id,
                    TaskType.CheckingBulk);

                if (checkingBulkTaskResult.IsFailure)
                {
                    logger.LogError(
                        "Failed to create checking bulk task for dispatch line {DispatchLineId}, product {ProductId}",
                        dispatchLine.Id,
                        line.ProductId);
                    return Result.Failure(checkingBulkTaskResult.Error);
                }

                taskRepository.Insert(checkingBulkTaskResult.Value);

                logger.LogInformation(
                    "Created checking bulk task {TaskId} for dispatch line {DispatchLineId}",
                    checkingBulkTaskResult.Value.Id,
                    dispatchLine.Id);
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation(
            "Successfully processed all dispatch lines for route trip {RouteTripId}",
            request.RouteTripId);

        return Result.Success();
    }

    private static bool IsProductBulk(Product product) => 
        !string.Equals(product.UnitOfMeasureName, "Each", StringComparison.OrdinalIgnoreCase) 
        || !string.Equals(product.UnitOfMeasureName, "Kg", StringComparison.OrdinalIgnoreCase);
}
