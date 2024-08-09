using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.PurchaseOrderLines;

namespace Crowbond.Modules.OMS.Application.PurchaseOrderLines.RemovePurchaseOrderLine;

internal sealed class RemovePurchaseOrderLineCommandHandler(
    IPurchaseOrderLineRepository purchaseOrderLineRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RemovePurchaseOrderLineCommand>
{
    public async Task<Result> Handle(RemovePurchaseOrderLineCommand request, CancellationToken cancellationToken)
    {
        PurchaseOrderLine? purchaseOrderLine = await purchaseOrderLineRepository.GetAsync(request.PurchaseOrderLineId, cancellationToken);

        if (purchaseOrderLine == null) 
        {
            return Result.Failure(PurchaseOrderLineErrors.NotFound(request.PurchaseOrderLineId));
        }

        purchaseOrderLine.PurchaseOrderHeader.RemoveLine(purchaseOrderLine.Id);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
