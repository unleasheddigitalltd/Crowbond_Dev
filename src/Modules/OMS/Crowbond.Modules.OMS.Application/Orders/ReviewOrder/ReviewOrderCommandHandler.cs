using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Orders;

namespace Crowbond.Modules.OMS.Application.Orders.ReviewOrder;

internal sealed class ReviewOrderCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ReviewOrderCommand>
{
    public async Task<Result> Handle(ReviewOrderCommand request, CancellationToken cancellationToken)
    {
        OrderHeader? orderHeader = await orderRepository.GetAsync(request.OrderHeaderId, cancellationToken);

        if (orderHeader is null)
        {
            return Result.Failure(OrderErrors.NotFound(request.OrderHeaderId));
        }

        Result result = orderHeader.Review();

        if (result.IsFailure)
        {
            return result;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
