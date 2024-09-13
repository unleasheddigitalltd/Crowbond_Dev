using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.PurchaseOrders;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.DraftPurchaseOrder;

internal sealed class DraftPurchaseOrderCommandHandler(
    IPurchaseOrderRepository purchaseOrderRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DraftPurchaseOrderCommand>
{
    public async Task<Result> Handle(DraftPurchaseOrderCommand request, CancellationToken cancellationToken)
    {
        PurchaseOrderHeader? purchaseOrderHeader = await purchaseOrderRepository.GetAsync(request.PurchaseOrderHeaderId, cancellationToken);

        if (purchaseOrderHeader is null)
        {
            return Result.Failure(PurchaseOrderErrors.NotFound(request.PurchaseOrderHeaderId));
        }

        Result<PurchaseOrderStatusHistory> result = purchaseOrderHeader.Draft(dateTimeProvider.UtcNow);
        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        purchaseOrderRepository.InsertHistory(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
