using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Locations;
using Crowbond.Modules.WMS.Domain.Receipts;
using Crowbond.Modules.WMS.Domain.Settings;
using Crowbond.Modules.WMS.Domain.Stocks;
using Crowbond.Modules.WMS.Domain.Tasks;
using Crowbond.Modules.WMS.Domain.WarehouseOperators;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.LocateProductForPutAway;
internal sealed class LocateProductForPutAwayCommandHandler(
    IWarehouseOperatorRepository warehouseOperatorRepository,
    IReceiptRepository receiptRepository,
    ITaskRepository taskRepository,
    ILocationRepository locationRepository,
    IStockRepository stockRepository,
    IDateTimeProvider dateTimeProvider,
    ISettingRepository settingRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<LocateProductForPutAwayCommand, Guid>
{
    public async Task<Result<Guid>> Handle(LocateProductForPutAwayCommand request, CancellationToken cancellationToken)
    {
        // Retrieve operator
        WarehouseOperator? warehouseOperator = await warehouseOperatorRepository.GetAsync(request.UserId, cancellationToken);

        if (warehouseOperator is null)
        {
            return Result.Failure<Guid>(WarehouseOperatorErrors.NotFound(request.UserId));
        }

        TaskHeader? taskHeader = await taskRepository.GetAsync(request.TaskHeaderId, cancellationToken);

        if (taskHeader is null)
        {
            return Result.Failure<Guid>(TaskErrors.NotFound(request.TaskHeaderId));            
        }

        if (!taskHeader.IsAssignedTo(request.UserId))
        {
            return Result.Failure<Guid>(TaskErrors.ActiveAssignmentForOperatorNotFound(request.UserId));            
        }
                
        // Retrieve receipt
        ReceiptHeader? receiptHeader = await receiptRepository.GetAsync(taskHeader.ReceiptId ?? Guid.Empty, cancellationToken);

        if (receiptHeader is null)
        {
            return Result.Failure<Guid>(ReceiptErrors.NotFound(taskHeader.ReceiptId ?? Guid.Empty));
        }

        ReceiptLine? receiptLine = receiptHeader.Lines.SingleOrDefault(l => l.Id == request.ReceiptLineId);

        if (receiptLine is null)
        {
            return Result.Failure<Guid>(ReceiptErrors.NotFound(taskHeader.ReceiptId ?? Guid.Empty));            
        }

        // Retrieve location
        Location? location = await locationRepository.GetAsync(request.LocationId, cancellationToken);

        if (location == null)
        {
            return Result.Failure<Guid>(LocationErrors.NotFound(request.LocationId));
        }

        // Retrieve setting
        Setting? setting = await settingRepository.GetAsync(cancellationToken);

        if (setting is null)
        {
            return Result.Failure<Guid>(SettingErrors.NotFound);
        }

        Result<TaskAssignmentLine> assignmentLineResult = taskHeader.AddAssignmentLine(
            request.ReceiptLineId, 
            null, 
            receiptHeader.LocationId ?? Guid.Empty, 
            request.LocationId, 
            receiptLine.ProductId, 
            request.Qty);

        if (assignmentLineResult.IsFailure)
        {
            return Result.Failure<Guid>(assignmentLineResult.Error);            
        }

        taskRepository.AddAssignmentLine(assignmentLineResult.Value);

        // Update stored qty in receipt line id
        receiptHeader.StoreLine(request.ReceiptLineId, request.Qty);

        // Retrieve destination stock
        IEnumerable<Stock> destStocks = await stockRepository.GetByLocationAsync(request.LocationId, cancellationToken);

        destStocks ??= Enumerable.Empty<Stock>();

        // check the possiblity of the destination stock.
        if (destStocks.FirstOrDefault(s => s.ReceiptLineId != receiptLine.Id && s.CurrentQty > 0) is not null && !setting.HasMixBatchLocation)
        {
            return Result.Failure<Guid>(StockErrors.LocationNotEmpty(request.LocationId));
        }

        // Check the existence of the destination stock.
        Stock? destStock = destStocks.FirstOrDefault(s => s.ReceiptLineId == receiptLine.Id);

        if (destStock is null)
        {
            Result<Stock> result = Stock.Create(
                receiptLine.ProductId,
                location.Id,
                receiptLine.BatchNumber,
                receiptHeader.ReceivedDate??DateOnly.MinValue,
                receiptLine.SellByDate,
                receiptLine.UseByDate,
                receiptLine.Id,
                null);

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            destStock = result.Value;
            stockRepository.InsertStock(destStock);
        }

        Result<StockTransaction> transResult = destStock.StockIn(assignmentLineResult.Value.Id, ActionType.PutAway.Name, dateTimeProvider.UtcNow, null, null, request.Qty);

        if (transResult.IsFailure)
        {
            return Result.Failure<Guid>(transResult.Error);
        }

        stockRepository.AddStockTransaction(transResult.Value);


        await unitOfWork.SaveChangesAsync(cancellationToken);

        return destStock.Id;
    }
}
