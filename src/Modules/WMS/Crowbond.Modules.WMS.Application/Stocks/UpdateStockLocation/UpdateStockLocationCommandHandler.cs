using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Stocks;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Locations;
using Crowbond.Modules.WMS.Domain.Products;
using Crowbond.Modules.WMS.Domain.Receipts;
using Crowbond.Modules.WMS.Domain.Settings;

namespace Crowbond.Modules.WMS.Application.Stocks.UpdateStockLocation;

internal sealed class UpdateStockLocationCommandHandler(
    IStockRepository stockRepository,
    IProductRepository productRepository,
    ILocationRepository locationRepository,
    IReceiptRepository receiptRepository,
    ISettingRepository settingRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateStockLocationCommand>
{
    public async Task<Result> Handle(UpdateStockLocationCommand request, CancellationToken cancellationToken)
    {
        StockTransactionReason? transactionReason = await stockRepository.GetTransactionReasonAsync(request.ReasonId, cancellationToken);

        if (transactionReason is null || transactionReason.ActionTypeName != ActionType.Relocating.Name)
        {
            return Result.Failure(StockErrors.ReasonNotFound(request.ReasonId));
        }

        Location? location = await locationRepository.GetAsync(request.Destination, cancellationToken);

        if (location == null)
        {
            return Result.Failure(StockErrors.LocationNotFound(request.Destination));
        }

        Stock? stock = await stockRepository.GetAsync(request.StockId, cancellationToken);

        if (stock is null)
        {
            return Result.Failure(StockErrors.NotFound(request.StockId));
        }

        Product? product = await productRepository.GetAsync(stock.ProductId, cancellationToken);

        if (product is null)
        {
            return Result.Failure(StockErrors.ProductNotFound(stock.ProductId));
        }

        ReceiptLine? receipt = await receiptRepository.GetReceiptLineAsync(stock.ReceiptId, cancellationToken);

        if (receipt is null)
        {
            return Result.Failure(StockErrors.ReceiptNotFound(stock.ReceiptId));
        }

        IEnumerable<Stock> destStocks = await stockRepository.GetByLocationAsync(request.Destination, cancellationToken);

        Setting? setting = await settingRepository.GetAsync(cancellationToken);

        if (setting is null)
        {
            return Result.Failure(StockErrors.SettingNotFound());
        }

        if (destStocks?.FirstOrDefault(s => s.BatchNumber != stock.BatchNumber) is not null && !setting.HasMixBatchLocation)
        {
            return Result.Failure(StockErrors.LocationNotEmpty(request.Destination));
        }

        Stock destinationStock = destStocks?.FirstOrDefault(s => s.BatchNumber == stock.BatchNumber);

        if (destinationStock is null)
        {
            destinationStock = Stock.Create(
                product,
                location,
                request.Quantity,
                request.Quantity,
                stock.BatchNumber,
                stock.ReceivedDate,
                stock.SellByDate,
                stock.UseByDate,
                receipt,
                stock.Note);

            stockRepository.InsertStock(destinationStock);
        }
        else
        {           
            destinationStock.Adjust(true, request.Quantity);
        }

        var originTransaction = StockTransaction.Create(
            null,
            ActionType.Relocating.Name,
            false,
            dateTimeProvider.UtcNow,
            request.TransactionNote,
            transactionReason,
            request.Quantity,
            product,
            stock);

        stockRepository.InsertStockTransaction(originTransaction);

        stock.Adjust(false, request.Quantity);


        var destTransaction = StockTransaction.Create(
            null,
            ActionType.Relocating.Name,
            true,
            dateTimeProvider.UtcNow,
            request.TransactionNote,
            transactionReason,
            request.Quantity,
            product,
            destinationStock);      

        stockRepository.InsertStockTransaction(destTransaction);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
