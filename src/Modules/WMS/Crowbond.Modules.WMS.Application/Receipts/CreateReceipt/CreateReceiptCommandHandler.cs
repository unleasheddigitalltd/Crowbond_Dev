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

        receiptRepository.Insert(result.Value);

        IEnumerable<ReceiptLine> receiptLines = request.Receipt.ReceiptLines
            .Select(r => ReceiptLine.Create(result.Value.Id, r.ProductId, r.QuantityReceived, r.UnitPrice));

        receiptRepository.AddLines(receiptLines);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
