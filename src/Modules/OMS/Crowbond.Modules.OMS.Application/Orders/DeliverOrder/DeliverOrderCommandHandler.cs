using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Drivers;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Domain.RouteTripLogs;

namespace Crowbond.Modules.OMS.Application.Orders.DeliverOrder;

internal sealed class DeliverOrderCommandHandler(
    IRouteTripLogRepository routeTripLogRepository,
    IDriverRepository driverRepository,
    IOrderRepository orderRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeliverOrderCommand, Guid>
{
    public async Task<Result<Guid>> Handle(DeliverOrderCommand request, CancellationToken cancellationToken)
    {
        Driver? driver = await driverRepository.GetAsync(request.DriverId, cancellationToken);

        if (driver == null)
        {
            return Result.Failure<Guid>(DriverErrors.NotFound(request.DriverId));
        }

        // getting the order
        OrderHeader? order = await orderRepository.GetAsync(request.OrderHeaderId, cancellationToken);
        if (order == null)
        {
            return Result.Failure<Guid>(OrderErrors.NotFound(request.OrderHeaderId));
        }

        RouteTripLog routeTripLog = await routeTripLogRepository.GetActiveByDriverIdAsync(driver.Id, cancellationToken);

        if (routeTripLog == null)
        {
            return Result.Failure<Guid>(RouteTripLogErrors.ActiveForDriverNotFound(driver.Id));
        }

        if (routeTripLog.RouteTripId != order.RouteTripId)
        {
            return Result.Failure<Guid>(OrderErrors.NotAssignedTo(routeTripLog.RouteTripId));            
        }

        if (routeTripLog.LoggedOnTime.Date != dateTimeProvider.UtcNow.Date)
        {
            return Result.Failure<Guid>(OrderErrors.LogDateMismatch(routeTripLog.RouteTripId));            
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
