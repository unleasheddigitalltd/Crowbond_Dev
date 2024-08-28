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

        Location? location = await locationRepository.GetAsync(request.Destination, cancellationToken);

        if (location == null)
        {
            return Result.Failure<Guid>(StockErrors.LocationNotFound(request.Destination));
        }

        Stock? stock = await stockRepository.GetAsync(request.StockId, cancellationToken);

        if (stock is null)
        {
            return Result.Failure<Guid>(StockErrors.NotFound(request.StockId));
        }

        Setting? setting = await settingRepository.GetAsync(cancellationToken);

        if (setting is null)
        {
            return Result.Failure<Guid>(StockErrors.SettingNotFound());
        }

        IEnumerable<Stock> destStocks = await stockRepository.GetByLocationAsync(request.Destination, cancellationToken);

        if (destStocks?.FirstOrDefault(s => s.BatchNumber != stock.BatchNumber) is not null && !setting.HasMixBatchLocation)
        {
            return Result.Failure<Guid>(StockErrors.LocationNotEmpty(request.Destination));
        }

        Result<Stock> destStockResult = destStocks?.FirstOrDefault(s => s.BatchNumber == stock.BatchNumber);

        if (destStocks?.FirstOrDefault(s => s.BatchNumber == stock.BatchNumber) is null)
        {
            destStockResult = Stock.Create(
                stock.ProductId,
                location.Id,
                request.Quantity,
                request.Quantity,
                stock.BatchNumber,
                stock.ReceivedDate,
                stock.SellByDate,
                stock.UseByDate,
                stock.ReceiptLineId,
                stock.Note,
                request.UserId,
                dateTimeProvider.UtcNow);

            if (destStockResult.IsFailure)
            {
                return Result.Failure<Guid>(destStockResult.Error);
            }

            stockRepository.InsertStock(destStockResult.Value);
        }
        else
        {
            destStockResult.Value.Adjust(request.UserId, dateTimeProvider.UtcNow, true, request.Quantity);
        }

        var originTransaction = StockTransaction.Create(
            null,
            ActionType.Relocating.Name,
            false,
            dateTimeProvider.UtcNow,
            request.TransactionNote,
            transactionReason.Id,
            request.Quantity,
            stock.ProductId,
            stock.Id);

        stockRepository.InsertStockTransaction(originTransaction);

        stock.Adjust(request.UserId, dateTimeProvider.UtcNow, false, request.Quantity);


        var destTransaction = StockTransaction.Create(
            null,
            ActionType.Relocating.Name,
            true,
            dateTimeProvider.UtcNow,
            request.TransactionNote,
            transactionReason.Id,
            request.Quantity,
            stock.ProductId,
            destStockResult.Value.Id);

        stockRepository.InsertStockTransaction(destTransaction);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return destStockResult.Value.Id;
    }
}
