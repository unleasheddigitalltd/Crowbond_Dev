using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Receipts;

namespace Crowbond.Modules.WMS.Application.Receipts.CancelReceipt;

internal sealed class CancelReceiptCommandHandler(
    IReceiptRepository receiptRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CancelReceiptCommand>
{
    public async Task<Result> Handle(CancelReceiptCommand request, CancellationToken cancellationToken)
    {
        ReceiptHeader? receiptHeader = await receiptRepository.GetByPurchaseOrderIdAsync(request.PurchaseOrderId, cancellationToken);

        if (receiptHeader is null)
        {
            return Result.Failure(ReceiptErrors.PurchaseOrderNotFound(request.PurchaseOrderId));
        }

        Result result = receiptHeader.Cancel();

        if (result.IsFailure)
        {
            return result;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
