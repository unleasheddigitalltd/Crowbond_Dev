using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.PurchaseOrderLines;

namespace Crowbond.Modules.OMS.Application.PurchaseOrderLines.UpdatePurchaseOrderLine;

internal sealed class UpdatePurchaseOrderLineCommandHandler(
    IPurchaseOrderLineRepository purchaseOrderLineRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdatePurchaseOrderLineCommand>
{
    public async Task<Result> Handle(UpdatePurchaseOrderLineCommand request, CancellationToken cancellationToken)
    {
        PurchaseOrderLine? purchaseOrderLine = await purchaseOrderLineRepository.GetAsync(request.PurchaseOrderLineId, cancellationToken);

        if (purchaseOrderLine is null)
        {
            return Result.Failure(PurchaseOrderLineErrors.NotFound(request.PurchaseOrderLineId));
        }

        purchaseOrderLine.Update(request.Qty, request.Comments);
        purchaseOrderLine.PurchaseOrderHeader.UpdateTotalAmount();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
