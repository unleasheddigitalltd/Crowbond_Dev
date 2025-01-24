using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Receipts;
using Crowbond.Modules.WMS.Domain.Sequences;

namespace Crowbond.Modules.WMS.Application.Receipts.CreateReceipt;

internal sealed class CreateReceiptCommandHandler(
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

        Result<ReceiptHeader> result = ReceiptHeader.Create(
            sequence.GetNumber(),
            request.PurchaseOrderId,
            request.PurchaseOrderNo);                     

        receiptRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
