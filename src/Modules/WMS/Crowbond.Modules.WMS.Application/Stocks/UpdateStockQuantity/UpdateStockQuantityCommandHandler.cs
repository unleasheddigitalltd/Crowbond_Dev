using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Stocks;
using Crowbond.Modules.WMS.Application.Abstractions.Data;

namespace Crowbond.Modules.WMS.Application.Stocks.UpdateStockQuantity;

internal sealed class UpdateStockQuantityCommandHandler(
    IStockRepository stockRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateStockQuantityCommand>
{
    public async Task<Result> Handle(UpdateStockQuantityCommand request, CancellationToken cancellationToken)
    {

        StockTransactionReason? transactionReason = await stockRepository.GetTransactionReasonAsync(request.ReasonId, cancellationToken);

        if (transactionReason is null || transactionReason.ActionTypeName != ActionType.Adjustment.Name)
        {
            return Result.Failure(StockErrors.ReasonNotFound(request.ReasonId));
        }

        Stock? stock = await stockRepository.GetAsync(request.StockId, cancellationToken);

        if (stock is null)
        {
            return Result.Failure(StockErrors.NotFound(request.StockId));
        }

        // Determine if the adjustment is positive or negative
        bool posAdjustment = stock.CurrentQty < request.Quantity;
        decimal quantity = Math.Abs(stock.CurrentQty - request.Quantity);

        // Apply stock adjustment
        Result<StockTransaction> result = posAdjustment
            ? stock.StockIn(
                null, 
                ActionType.Adjustment.Name, 
                dateTimeProvider.UtcNow, 
                request.TransactionNote,
                transactionReason.Id, 
                quantity)
            : stock.StockOut(
                null, 
                ActionType.Adjustment.Name, 
                dateTimeProvider.UtcNow, 
                request.TransactionNote,
                transactionReason.Id, 
                quantity);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        stockRepository.AddStockTransaction(result.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

