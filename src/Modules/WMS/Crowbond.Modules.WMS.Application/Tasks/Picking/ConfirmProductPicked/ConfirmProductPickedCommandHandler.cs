using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Locations;
using Crowbond.Modules.WMS.Domain.Stocks;
using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.ConfirmProductPicked;

internal sealed class ConfirmProductPickedCommandHandler(
    ITaskRepository taskRepository,
    IStockRepository stockRepository,
    ILocationRepository locationRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ConfirmProductPickedCommand, Guid>
{
    public async Task<Result<Guid>> Handle(ConfirmProductPickedCommand request, CancellationToken cancellationToken)
    {
        TaskAssignmentLine? assignmentLine = await taskRepository.GetAssignmentLineAsync(request.TaskAssignmentLineId, cancellationToken);

        if (assignmentLine is null)
        {
            return Result.Failure<Guid>(TaskErrors.AssignmentLineNotFound(request.TaskAssignmentLineId));
        }

        TaskHeader taskHeader = assignmentLine.Assignment.Header;

        if (!taskHeader.IsAssignedTo(request.UserId))
        {
            return Result.Failure<Guid>(TaskErrors.ActiveAssignmentForOperatorNotFound(request.UserId));
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

        if (request.Qty > assignmentLine.RequestedQty - assignmentLine.CompletedQty)
        {
            return Result.Failure<Guid>(TaskErrors.ExceedsRequestedQuantity);
        }

        Location? location = await locationRepository.GetAsync(request.ToLocationId, cancellationToken);

        if (location == null)
        {
            return Result.Failure<Guid>(StockErrors.LocationNotFound(request.ToLocationId));
        }

        if (location.LocationType != LocationType.TransferUtility)
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
            request.TaskAssignmentLineId,
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
            request.TaskAssignmentLineId,
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

        taskHeader.IncrementCompletedQty(dateTimeProvider.UtcNow, assignmentLine.ProductId, request.Qty);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return destStock.Id;
    }
}
