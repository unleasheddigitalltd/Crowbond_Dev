using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Receipts;

namespace Crowbond.Modules.WMS.Application.Receipts.RemoveReceiptLine;

internal sealed class RemoveReceiptLineCommandHandler(
    IReceiptRepository receiptRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveReceiptLineCommand>
{
    public async Task<Result> Handle(RemoveReceiptLineCommand request, CancellationToken cancellationToken)
    {
        ReceiptLine? receiptLine = await receiptRepository.GetLineAsync(request.ReceiptLineId, cancellationToken);

        if (receiptLine == null)
        {
            return Result.Failure(ReceiptErrors.LineNotFound(request.ReceiptLineId));
        }

        ReceiptHeader? receipt = await receiptRepository.GetAsync(receiptLine.ReceiptHeaderId, cancellationToken);

        if (receipt == null)
        {
            return Result.Failure(ReceiptErrors.NotFound(receiptLine.ReceiptHeaderId));
        }

        Result result = receipt.RemoveLine(request.ReceiptLineId);

        if (result.IsFailure)
        {
            return result;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
