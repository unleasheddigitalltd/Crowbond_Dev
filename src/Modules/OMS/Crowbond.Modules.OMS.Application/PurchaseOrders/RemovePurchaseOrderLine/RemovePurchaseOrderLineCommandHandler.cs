using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.PurchaseOrders;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.RemovePurchaseOrderLine;

internal sealed class RemovePurchaseOrderLineCommandHandler(
    IPurchaseOrderRepository purchaseOrderRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RemovePurchaseOrderLineCommand>
{
    public async Task<Result> Handle(RemovePurchaseOrderLineCommand request, CancellationToken cancellationToken)
    {
        PurchaseOrderLine purchaseOrderLine = await purchaseOrderRepository.GetLineAsync(request.PurchaseOrderLineId, cancellationToken);

        if (purchaseOrderLine == null)
        {
            return Result.Failure(PurchaseOrderErrors.LineNotFound(request.PurchaseOrderLineId));
        }

        PurchaseOrderHeader purchaseOrder = await purchaseOrderRepository.GetAsync(purchaseOrderLine.PurchaseOrderHeaderId, cancellationToken);

        if (purchaseOrder == null)
        {
            return Result.Failure(PurchaseOrderErrors.NotFound(purchaseOrderLine.PurchaseOrderHeaderId));
        }

        Result result = purchaseOrder.RemoveLine(purchaseOrderLine.Id);

        if (result.IsFailure)
        {
            return result;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
