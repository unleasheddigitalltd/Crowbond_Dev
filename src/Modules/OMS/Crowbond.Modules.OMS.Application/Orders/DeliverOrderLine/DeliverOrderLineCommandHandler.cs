using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Drivers;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Domain.RouteTripLogs;
using Crowbond.Modules.OMS.Domain.RouteTrips;

namespace Crowbond.Modules.OMS.Application.Orders.DeliverOrderLine;

internal sealed class DeliverOrderLineCommandHandler(
    IDriverRepository driverRepository,
    IRouteTripRepository routeTripRepository,
    IRouteTripLogRepository routeTripLogRepository,
    IOrderRepository orderRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeliverOrderLineCommand>
{
    public async Task<Result> Handle(DeliverOrderLineCommand request, CancellationToken cancellationToken)
    {
        var currevtDate = DateOnly.FromDateTime(dateTimeProvider.UtcNow);

        Driver? driver = await driverRepository.GetAsync(request.DriverId, cancellationToken);

        if (driver is null)
        {
            return Result.Failure(DriverErrors.NotFound(request.DriverId));
        }

        OrderLine? orderLine = await orderRepository.GetLineAsync(request.OrderLineId, cancellationToken);

        if (orderLine is null)
        {
            return Result.Failure(OrderErrors.LineNotFound(request.OrderLineId));            
        }

        if (orderLine.ActualQty != request.DeliveredQty && request.RejectReasonId == null)
        {
            return Result.Failure(OrderErrors.MissingRejectReason);            
        }

        OrderHeader orderHeader = orderLine.Header;
        // check order is assigned to a route trip.
        if (orderHeader.RouteTripId is null)
        {
            return Result.Failure<Guid>(OrderErrors.NotAssignedToRouteTrip(orderHeader.Id));
        }

        RouteTrip? routeTrip = await routeTripRepository.GetAsync((Guid)orderHeader.RouteTripId, cancellationToken);

        if (routeTrip == null)
        {
            return Result.Failure<Guid>(RouteTripErrors.NotFound((Guid)orderHeader.RouteTripId));
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
            return Result.Failure<Guid>(RouteTripErrors.NotAvailable(routeTrip.Id));
        }

        Result result = orderHeader.DeliverLine(orderLine.Id, request.DeliveredQty, request.RejectReasonId, request.DeliveryComments);

        if (result.IsFailure)
        {
            return result;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
