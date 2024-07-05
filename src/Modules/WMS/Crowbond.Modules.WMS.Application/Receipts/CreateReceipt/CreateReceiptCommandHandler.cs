using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Receipts;

namespace Crowbond.Modules.WMS.Application.Receipts.CreateReceipt;

internal sealed class CreateReceiptCommandHandler(
    IDateTimeProvider dateTimeProvider,
    IReceiptRepository receiptRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateReceiptCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateReceiptCommand request, CancellationToken cancellationToken)
    {
        Result<ReceiptHeader> result = ReceiptHeader.Create(
            request.Receipt.ReceivedDate,
            request.Receipt.PurchaseOrderId,
            request.Receipt.DeliveryNoteNumber,
            dateTimeProvider.UtcNow);

        receiptRepository.InsertReceiptHeader(result.Value);

        IEnumerable<ReceiptLine> receiptLines = request.Receipt.ReceiptLines
            .Select(r => ReceiptLine.Create(result.Value.Id, r.ProductId, r.QuantityReceived, r.UnitPrice, r.SellByDate, r.UseByDate));

        receiptRepository.InsertRangeReceiptLines(receiptLines);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
