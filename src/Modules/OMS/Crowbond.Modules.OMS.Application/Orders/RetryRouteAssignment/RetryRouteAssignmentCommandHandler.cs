using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.PublicApi;

namespace Crowbond.Modules.OMS.Application.Orders.RetryRouteAssignment;

internal sealed class RetryRouteAssignmentCommandHandler(
    IRouteTripApi routeTripApi,
    IOrderRepository orderRepository) 
    : ICommandHandler<RetryRouteAssignmentCommand>
{
    public async Task<Result> Handle(RetryRouteAssignmentCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetAsync(request.OrderId, cancellationToken);
        
        if (order is null)
        {
            return Result.Failure(OrderErrors.NotFound(request.OrderId));
        }

        await routeTripApi.AssignRouteTripToOrderAsync(order.Id, cancellationToken);
        return Result.Success();
    }
}
