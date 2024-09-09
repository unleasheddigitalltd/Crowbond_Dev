using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Stocks;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Locations;
using Crowbond.Modules.WMS.Domain.Settings;

namespace Crowbond.Modules.WMS.Application.Stocks.UpdateStockLocation;

internal sealed class UpdateStockLocationCommandHandler(
    IStockRepository stockRepository,
    ILocationRepository locationRepository,
    ISettingRepository settingRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateStockLocationCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UpdateStockLocationCommand request, CancellationToken cancellationToken)
    {
        StockTransactionReason? transactionReason = await stockRepository.GetTransactionReasonAsync(request.ReasonId, cancellationToken);

        if (transactionReason is null || transactionReason.ActionTypeName != ActionType.Relocating.Name)
        {
            return Result.Failure<Guid>(StockErrors.ReasonNotFound(request.ReasonId));
        }

        Stock? stock = await stockRepository.GetAsync(request.StockId, cancellationToken);

        if (stock is null)
        {
            return Result.Failure<Guid>(StockErrors.NotFound(request.StockId));
        }

        if (stock.LocationId == request.Destination)
        {
            return Result.Failure<Guid>(StockErrors.SameLocation);
        }

        Location? location = await locationRepository.GetAsync(request.Destination, cancellationToken);

        if (location == null)
        {
            return Result.Failure<Guid>(StockErrors.LocationNotFound(request.Destination));
        }

        Setting? setting = await settingRepository.GetAsync(cancellationToken);

        if (setting is null)
        {
            return Result.Failure<Guid>(StockErrors.SettingNotFound);
        }

        IEnumerable<Stock> destStocks = await stockRepository.GetByLocationAsync(request.Destination, cancellationToken);

        destStocks ??= Enumerable.Empty<Stock>();

        // check the possiblity of the destination.
        if (destStocks.FirstOrDefault(s => s.ReceiptLineId != stock.ReceiptLineId && s.CurrentQty > 0) is not null && !setting.HasMixBatchLocation)
        {
            return Result.Failure<Guid>(StockErrors.LocationNotEmpty(request.Destination));
        }

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
            null,
            ActionType.Relocating.Name,
            dateTimeProvider.UtcNow,
            request.TransactionNote,
            transactionReason.Id,
            request.Quantity);

        if (orgTransResult.IsFailure)
        {
            return Result.Failure<Guid>(orgTransResult.Error);
        }

        Result<StockTransaction> destTransResult = destStock.StockIn(
            null,
            ActionType.Relocating.Name,
            dateTimeProvider.UtcNow,
            request.TransactionNote,
            transactionReason.Id,
            request.Quantity);

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
