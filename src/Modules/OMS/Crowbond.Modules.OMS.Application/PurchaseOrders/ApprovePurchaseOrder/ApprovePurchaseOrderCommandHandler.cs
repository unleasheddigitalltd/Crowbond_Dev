using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.PurchaseOrders;
using Crowbond.Modules.OMS.Domain.Sequences;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.ApprovePurchaseOrder;

internal sealed class ApprovePurchaseOrderCommandHandler(
    IPurchaseOrderRepository purchaseOrderRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ApprovePurchaseOrderCommand>
{
    public async Task<Result> Handle(ApprovePurchaseOrderCommand request, CancellationToken cancellationToken)
    {
        PurchaseOrderHeader? purchaseOrderHeader = await purchaseOrderRepository.GetAsync(request.PurchaseOrderHeaderId, cancellationToken);

        if (purchaseOrderHeader is null)
        {
            return Result.Failure(PurchaseOrderErrors.NotFound(request.PurchaseOrderHeaderId));
        }

        Sequence? sequence = await purchaseOrderRepository.GetSequenceAsync(cancellationToken);

        if (sequence is null)
        {
            return Result.Failure(PurchaseOrderErrors.SequenceNotFound());
        }

        Result<PurchaseOrderStatusHistory> result = purchaseOrderHeader.Approve(sequence.GetNumber(), dateTimeProvider.UtcNow);
        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        purchaseOrderRepository.InsertHistory(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
