using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Dispatches;
using Crowbond.Modules.WMS.Domain.Receipts;
using Crowbond.Modules.WMS.Domain.Sequences;

namespace Crowbond.Modules.WMS.Application.Dispatches.CreateDispatch;
internal sealed class CreateDispatchCommandHandler(
    IDispatchRepository dispatchRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateDispatchCommand>
{
    public async Task<Result> Handle(CreateDispatchCommand request, CancellationToken cancellationToken)
    {
        Sequence? dispatchSeq = await dispatchRepository.GetSequenceAsync(cancellationToken);

        if (dispatchSeq is null)
        {
            return Result.Failure<Guid>(ReceiptErrors.SequenceNotFound());
        }

        Result<DispatchHeader> dispatchResult = DispatchHeader.Create(
            dispatchSeq.GetNumber(),
            request.RouteTripId,
            request.RouteTripDate,
            request.RouteName);

        dispatchRepository.Insert(dispatchResult.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
