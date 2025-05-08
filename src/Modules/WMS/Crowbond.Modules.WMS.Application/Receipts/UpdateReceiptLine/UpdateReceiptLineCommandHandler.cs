using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Receipts;

namespace Crowbond.Modules.WMS.Application.Receipts.UpdateReceiptLine;

internal sealed class UpdateReceiptLineCommandHandler(
    IReceiptRepository receiptRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateReceiptLineCommand>
{
    public async Task<Result> Handle(UpdateReceiptLineCommand request, CancellationToken cancellationToken)
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

        var result = receipt.UpdateLine(
           request.ReceiptLineId, request.QuantityReceived, request.Batch ?? string.Empty, request.SellByDate, request.UseByDate);

        if (result.IsFailure)
        {
            return result;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
