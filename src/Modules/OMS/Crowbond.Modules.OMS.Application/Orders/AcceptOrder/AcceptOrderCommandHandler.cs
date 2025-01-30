using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Domain.RouteTrips;

namespace Crowbond.Modules.OMS.Application.Orders.AcceptOrder;

internal sealed class AcceptOrderCommandHandler(
    IOrderRepository orderRepository,
    IRouteTripRepository routeTripRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AcceptOrderCommand>
{
    public async Task<Result> Handle(AcceptOrderCommand request, CancellationToken cancellationToken)
    {

        OrderHeader? orderHeader = await orderRepository.GetAsync(request.OrderId, cancellationToken);

        if (orderHeader is null)
        {
            return Result.Failure(OrderErrors.NotFound(request.OrderId));
        }

        if (orderHeader.RouteTripId is null)
        {
            return Result.Failure(OrderErrors.NoRouteTripHasAssigned(request.OrderId));
        }

        RouteTrip? routeTrip = await routeTripRepository.GetAsync((Guid)orderHeader.RouteTripId, cancellationToken);

        if (routeTrip is null)
        {
            return Result.Failure(RouteTripErrors.NotFound((Guid)orderHeader.RouteTripId));
        }

        // Add get available quantity and check the avalable quantity here if nventoryService.GetAvailableQuantityAsync is implemented

        Result result = orderHeader.Accept();

        if (result.IsFailure)
        {
            return result;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
