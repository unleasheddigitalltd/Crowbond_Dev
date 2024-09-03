using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Receipts;
using Crowbond.Modules.WMS.Domain.Sequences;

namespace Crowbond.Modules.WMS.Application.Receipts.CreateReceipt;

internal sealed class CreateReceiptCommandHandler(
    IDateTimeProvider dateTimeProvider,
    IReceiptRepository receiptRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateReceiptCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateReceiptCommand request, CancellationToken cancellationToken)
    {
        Sequence? sequence = await receiptRepository.GetSequenceAsync(cancellationToken);

        if (sequence is null)
        {
            return Result.Failure<Guid>(ReceiptErrors.SequenceNotFound());
        }

        string receiptNo = $"{sequence.Prefix}-{sequence.GetNewSequence()}";

        Result<ReceiptHeader> result = ReceiptHeader.Create(
            receiptNo,
            request.Receipt.ReceivedDate,
            request.Receipt.PurchaseOrderId,
            request.Receipt.PurchaseOrderNo,
            request.Receipt.DeliveryNoteNumber,
            request.Receipt.CreateBy,
            dateTimeProvider.UtcNow);


        foreach (ReceiptRequest.ReceiptLineRequest line in request.Receipt.ReceiptLines)
        {
            result.Value.AddLine(
                request.Receipt.CreateBy,
                dateTimeProvider.UtcNow,
                line.ProductId, 
                line.QuantityReceived,
                line.UnitPrice);
        }

        receiptRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
