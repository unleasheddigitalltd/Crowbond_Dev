using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Dispatches;
using Crowbond.Modules.WMS.Domain.Products;
using Crowbond.Modules.WMS.Domain.Sequences;
using Crowbond.Modules.WMS.Domain.Tasks;
using Crowbond.Modules.WMS.Application.Tasks;
using Microsoft.Extensions.Logging;

namespace Crowbond.Modules.WMS.Application.Dispatches.AddDispatchLines;

internal sealed class AddDispatchLinesCommandHandler(
    IDispatchRepository dispatchRepository,
    IProductRepository productRepository,
    ConsolidatedPickingTaskService consolidatedPickingTaskService,
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

        // Remove old task creation logic - we'll use consolidated picking at the end

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
        }

        // Save dispatch lines first
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation(
            "All dispatch lines saved. Creating consolidated picking tasks for dispatch {DispatchId}",
            dispatchHeader.Id);

        // Create consolidated picking tasks
        var consolidatedTaskResult = await consolidatedPickingTaskService.AddDispatchToPickingTasks(
            dispatchHeader, 
            cancellationToken);

        if (consolidatedTaskResult.IsFailure)
        {
            logger.LogError(
                "Failed to create consolidated picking tasks for dispatch {DispatchId}: {Error}",
                dispatchHeader.Id,
                consolidatedTaskResult.Error);
            return consolidatedTaskResult;
        }

        logger.LogInformation(
            "Successfully created consolidated picking tasks for dispatch {DispatchId}",
            dispatchHeader.Id);

        logger.LogInformation(
            "Successfully processed all dispatch lines for route trip {RouteTripId}",
            request.RouteTripId);

        return Result.Success();
    }

    private static bool IsProductBulk(Product product) => 
        !string.Equals(product.UnitOfMeasureName, "Each", StringComparison.OrdinalIgnoreCase) 
        || !string.Equals(product.UnitOfMeasureName, "Kg", StringComparison.OrdinalIgnoreCase);
}
