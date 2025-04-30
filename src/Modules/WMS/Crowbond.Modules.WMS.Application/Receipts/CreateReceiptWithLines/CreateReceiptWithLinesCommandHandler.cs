using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Receipts;
using Crowbond.Modules.WMS.Domain.Sequences;

namespace Crowbond.Modules.WMS.Application.Receipts.CreateReceiptWithLines;

internal sealed class CreateReceiptWithLinesCommandHandler(
    IReceiptRepository receiptRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateReceiptWithLinesCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateReceiptWithLinesCommand request, CancellationToken cancellationToken)
    {
        Sequence? sequence = await receiptRepository.GetSequenceAsync(cancellationToken);

        if (sequence is null)
        {
            return Result.Failure<Guid>(ReceiptErrors.SequenceNotFound());
        }

        Result<ReceiptHeader> result = ReceiptHeader.Create(
            sequence.GetNumber(),
            request.Receipt.PurchaseOrderId,
            request.Receipt.PurchaseOrderNo);

        foreach (ReceiptRequest.ReceiptLineRequest line in request.Receipt.ReceiptLines)
        {
            result.Value.AddLine(
                line.ProductId,
                line.QuantityReceived,
                line.UnitPrice,
                line.BatchNumber,
                line.SellByDate,
                line.UseByDate);
        }

        receiptRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
