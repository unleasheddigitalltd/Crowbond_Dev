using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Drivers;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Domain.RouteTripLogs;
using Crowbond.Modules.OMS.Domain.RouteTrips;

namespace Crowbond.Modules.OMS.Application.Orders.DeliverOrder;

internal sealed class DeliverOrderCommandHandler(
    IRouteTripRepository routeTripRepository,
    IRouteTripLogRepository routeTripLogRepository,
    IDriverRepository driverRepository,
    IOrderRepository orderRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeliverOrderCommand, Guid>
{
    public async Task<Result<Guid>> Handle(DeliverOrderCommand request, CancellationToken cancellationToken)
    {
        var currevtDate = DateOnly.FromDateTime(dateTimeProvider.UtcNow);
        Driver ? driver = await driverRepository.GetAsync(request.DriverId, cancellationToken);

        if (driver == null)
        {
            return Result.Failure<Guid>(DriverErrors.NotFound(request.DriverId));
        }

        OrderHeader? order = await orderRepository.GetAsync(request.OrderHeaderId, cancellationToken);
        if (order == null)
        {
            return Result.Failure<Guid>(OrderErrors.NotFound(request.OrderHeaderId));
        }

        // check order is assigned to a route trip.
        if (order.RouteTripId is null)
        {
            return Result.Failure<Guid>(OrderErrors.NotAssignedToRouteTrip(request.OrderHeaderId));
        }

        RouteTrip? routeTrip = await routeTripRepository.GetAsync((Guid)order.RouteTripId, cancellationToken);

        if (routeTrip == null)
        {
            return Result.Failure<Guid>(RouteTripErrors.NotFound((Guid)order.RouteTripId));
        }

        RouteTripLog? routeTripLog = await routeTripLogRepository.GetActiveByDateAndDriverAndRouteTrip(currevtDate, routeTrip.Id, driver.Id, cancellationToken);

        if (routeTripLog == null)
        {
            return Result.Failure<Guid>(RouteTripLogErrors.NoActiveLog(routeTrip.Id));
        }

        // check the active log is belong to this driver.
        if (routeTripLog.DriverId != driver.Id)
        {
            return Result.Failure<Guid>(RouteTripLogErrors.InvalidDriverLog(routeTrip.Id));
        }

        // check route trip is available.
        if (routeTrip.Status != RouteTripStatus.Available)
        {
            return Result.Failure<Guid>(RouteTripErrors.InvalidStatus(RouteTripStatus.Available));
        }

        // create the delivery
        Result<OrderDelivery> deliveryResult = order.Deliver(routeTripLog.Id, dateTimeProvider.UtcNow, request.Comments);

        if (deliveryResult.IsFailure)
        {
            return Result.Failure<Guid>(deliveryResult.Error);
        }

        orderRepository.AddDelivery(deliveryResult.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(deliveryResult.Value.Id);
    }
}
