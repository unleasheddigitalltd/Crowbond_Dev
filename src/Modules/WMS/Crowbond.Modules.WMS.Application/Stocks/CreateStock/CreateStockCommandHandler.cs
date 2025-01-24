using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Locations;
using Crowbond.Modules.WMS.Domain.Receipts;
using Crowbond.Modules.WMS.Domain.Settings;
using Crowbond.Modules.WMS.Domain.Stocks;

namespace Crowbond.Modules.WMS.Application.Stocks.CreateStock;
internal sealed class CreateStockCommandHandler(
    IStockRepository stockRepository,
    IReceiptRepository receiptRepository,
    ILocationRepository locationRepository,
    ISettingRepository settingRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateStockCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateStockCommand request, CancellationToken cancellationToken)
    {
        ReceiptLine? receiptLine = await receiptRepository.GetLineAsync(request.ReceiptLineId, cancellationToken);

        if (receiptLine is null)
        {
            return Result.Failure<Guid>(ReceiptErrors.LineNotFound(request.ReceiptLineId));
        }

        // Retrieve receipt
        ReceiptHeader? receiptHeader = await receiptRepository.GetAsync(receiptLine.ReceiptHeaderId, cancellationToken);

        if (receiptHeader is null)
        {
            return Result.Failure<Guid>(ReceiptErrors.NotFound(receiptLine.ReceiptHeaderId));
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

        // Update stored qty in receipt line id
        Result addReceiptLineResult = receiptHeader.StoreLine(request.ReceiptLineId, request.Qty);
        if (addReceiptLineResult.IsFailure)
        {
            return Result.Failure<Guid>(addReceiptLineResult.Error);
        }

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
                receiptHeader.ReceivedDate ?? DateOnly.MinValue,
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

        Result<StockTransaction> transResult = destStock.StockIn(null, ActionType.BlinedReceive.Name, dateTimeProvider.UtcNow, null, null, request.Qty);

        if (transResult.IsFailure)
        {
            return Result.Failure<Guid>(transResult.Error);
        }

        stockRepository.AddStockTransaction(transResult.Value);


        await unitOfWork.SaveChangesAsync(cancellationToken);

        return destStock.Id;
    }
}
