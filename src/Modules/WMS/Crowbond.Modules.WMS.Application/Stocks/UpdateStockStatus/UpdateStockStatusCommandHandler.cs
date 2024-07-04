using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Stocks;
using Crowbond.Common.Application.Extentions;

namespace Crowbond.Modules.WMS.Application.Stocks.UpdateStockStatus;

internal sealed class UpdateStockStatusCommandHandler(
    IStockRepository stockRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateStockStatusCommand>
{
    public async Task<Result> Handle(UpdateStockStatusCommand request, CancellationToken cancellationToken)
    {
        if (!request.StatusType.TryConvertToEnum<StockStatus>(out StockStatus statusType))
        {
            return Result.Failure(StockErrors.StatusNotFound(request.StatusType));
        }

        Stock? stock = await stockRepository.GetAsync(request.StockId, cancellationToken);

        if (stock is null)
        {
            return Result.Failure(StockErrors.NotFound(request.StockId));
        }

        var stockActions = new Dictionary<StockStatus, Func<Result>>
        {
            { StockStatus.Active, stock.Activate },
            { StockStatus.Held, stock.Hold },
            { StockStatus.Damaged, stock.MarkAsDamaged }
        };

        Result result = stockActions[statusType]();

        if (result.IsFailure)
        {
            return result;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
