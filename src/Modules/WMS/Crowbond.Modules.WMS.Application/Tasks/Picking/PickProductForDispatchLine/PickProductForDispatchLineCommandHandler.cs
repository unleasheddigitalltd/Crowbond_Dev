using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Dispatches;
using Crowbond.Modules.WMS.Domain.Locations;
using Crowbond.Modules.WMS.Domain.Receipts;
using Crowbond.Modules.WMS.Domain.Stocks;
using Crowbond.Modules.WMS.Domain.Tasks;
using Crowbond.Modules.WMS.Domain.WarehouseOperators;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.PickProductForDispatchLine;

internal sealed class PickProductForDispatchLineCommandHandler(
    IWarehouseOperatorRepository warehouseOperatorRepository,
    IDispatchRepository dispatchRepository,
    ITaskRepository taskRepository,
    ILocationRepository locationRepository,
    IStockRepository stockRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<PickProductForDispatchLineCommand, Guid>
{
    public async Task<Result<Guid>> Handle(PickProductForDispatchLineCommand request, CancellationToken cancellationToken)
    {
        // Retrieve operator
        WarehouseOperator? warehouseOperator = await warehouseOperatorRepository.GetAsync(request.UserId, cancellationToken);

        if (warehouseOperator is null)
        {
            return Result.Failure<Guid>(WarehouseOperatorErrors.NotFound(request.UserId));
        }

        // Retrieve task
        TaskHeader? taskHeader = await taskRepository.GetAsync(request.TaskHeaderId, cancellationToken);

        if (taskHeader is null)
        {
            return Result.Failure<Guid>(TaskErrors.NotFound(request.TaskHeaderId));
        }

        if (!taskHeader.IsAssignedTo(request.UserId))
        {
            return Result.Failure<Guid>(TaskErrors.ActiveAssignmentForOperatorNotFound(request.UserId));
        }

        // Retrieve dispatch
        DispatchHeader? dispatchHeader = await dispatchRepository.GetAsync(taskHeader.DispatchId ?? Guid.Empty, cancellationToken);

        if (dispatchHeader is null)
        {
            return Result.Failure<Guid>(ReceiptErrors.NotFound(taskHeader.ReceiptId ?? Guid.Empty));
        }

        DispatchLine? dispatchLine = dispatchHeader.Lines.SingleOrDefault(l => l.Id == request.DispatchLineId);

        if (dispatchLine is null)
        {
            return Result.Failure<Guid>(ReceiptErrors.NotFound(taskHeader.ReceiptId ?? Guid.Empty));
        }

        // Pick the product
        Stock? stock = await stockRepository.GetAsync(request.StockId, cancellationToken);

        if (stock is null)
        {
            return Result.Failure<Guid>(StockErrors.NotFound(request.StockId));
        }

        if (request.Qty > stock.CurrentQty)
        {
            return Result.Failure<Guid>(TaskErrors.ExceedsAvailableStock);
        }

        if (request.Qty > dispatchLine.OrderedQty - dispatchLine.PickedQty)
        {
            return Result.Failure<Guid>(TaskErrors.ExceedsRequestedQuantity);
        }

        Result<TaskAssignmentLine> taskAssignmentLineResult = taskHeader.AddAssignmentLine(null, request.DispatchLineId, stock.LocationId, request.ToLocationId, dispatchLine.ProductId, request.Qty);

        if (taskAssignmentLineResult.IsFailure)
        {
                return Result.Failure<Guid>(taskAssignmentLineResult.Error);
        }

        taskRepository.AddAssignmentLine(taskAssignmentLineResult.Value);

        Result pickResult = dispatchHeader.PickLine(dispatchLine.Id, request.Qty);

        if (pickResult.IsFailure)
        {
            return Result.Failure<Guid>(pickResult.Error);
        }

        Location? location = await locationRepository.GetAsync(request.ToLocationId, cancellationToken);

        if (location == null)
        {
            return Result.Failure<Guid>(StockErrors.LocationNotFound(request.ToLocationId));
        }

        if (location.LocationType != LocationType.PackStation)
        {
            return Result.Failure<Guid>(LocationErrors.NotTransfereUtility(request.ToLocationId));
        }

        IEnumerable<Stock> destStocks = await stockRepository.GetByLocationAsync(request.ToLocationId, cancellationToken);

        destStocks ??= Enumerable.Empty<Stock>();

        // Check the existence of the destination.
        Stock? destStock = destStocks.FirstOrDefault(s => s.ReceiptLineId == stock.ReceiptLineId);

        if (destStock is null)
        {
            Result<Stock> result = Stock.Create(
                stock.ProductId,
                location.Id,
                stock.BatchNumber,
                stock.ReceivedDate,
                stock.SellByDate,
                stock.UseByDate,
                stock.ReceiptLineId,
                stock.Note);

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            destStock = result.Value;
            stockRepository.InsertStock(destStock);
        }

        Result<StockTransaction> orgTransResult = stock.StockOut(
            taskAssignmentLineResult.Value.Id,
            ActionType.Picking.Name,
            dateTimeProvider.UtcNow,
            null,
            null,
            request.Qty);

        if (orgTransResult.IsFailure)
        {
            return Result.Failure<Guid>(orgTransResult.Error);
        }

        Result<StockTransaction> destTransResult = destStock.StockIn(
            taskAssignmentLineResult.Value.Id,
            ActionType.Picking.Name,
            dateTimeProvider.UtcNow,
            null,
            null,
            request.Qty);

        if (destTransResult.IsFailure)
        {
            return Result.Failure<Guid>(destTransResult.Error);
        }

        stockRepository.AddStockTransaction(orgTransResult.Value);
        stockRepository.AddStockTransaction(destTransResult.Value);


        await unitOfWork.SaveChangesAsync(cancellationToken);

        return destStock.Id;
    }
}
