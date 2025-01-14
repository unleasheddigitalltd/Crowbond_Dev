using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.PurchaseOrders;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.UpdatePurchaseOrderLine;

internal sealed class UpdatePurchaseOrderLineCommandHandler(
    IPurchaseOrderRepository purchaseOrderRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdatePurchaseOrderLineCommand>
{
    public async Task<Result> Handle(UpdatePurchaseOrderLineCommand request, CancellationToken cancellationToken)
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

        Result result = purchaseOrder.UpdateLine(purchaseOrderLine.Id, request.UnitPrice, request.Qty, request.Comments);

        if (result.IsFailure)
        {
            return result;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
