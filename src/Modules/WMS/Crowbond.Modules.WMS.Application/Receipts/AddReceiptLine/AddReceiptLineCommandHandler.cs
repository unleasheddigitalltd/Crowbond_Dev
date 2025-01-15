using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Application.Receipts.CreateReceipt;
using Crowbond.Modules.WMS.Domain.Receipts;

namespace Crowbond.Modules.WMS.Application.Receipts.AddReceiptLine;

internal sealed class AddReceiptLineCommandHandler(
    IReceiptRepository receiptRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddReceiptLineCommand, Guid>
{
    public async Task<Result<Guid>> Handle(AddReceiptLineCommand request, CancellationToken cancellationToken)
    {
        ReceiptHeader? receipt = await receiptRepository.GetAsync(request.ReceiptHeaderId, cancellationToken);

        if (receipt == null)
        {
            return Result.Failure<Guid>(ReceiptErrors.NotFound(request.ReceiptHeaderId));
        }

        Result<ReceiptLine> result = receipt.AddLine(
            request.ProductId, 
            request.QuantityReceived, 
            request.UnitPrice);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        receiptRepository.AddLine(result.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(result.Value.Id);
    }
}
