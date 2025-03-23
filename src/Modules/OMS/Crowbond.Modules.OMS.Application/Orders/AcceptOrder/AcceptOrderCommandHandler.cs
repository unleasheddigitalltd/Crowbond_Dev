using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Domain.RouteTrips;
using Microsoft.Extensions.Logging;

namespace Crowbond.Modules.OMS.Application.Orders.AcceptOrder;

internal sealed class AcceptOrderCommandHandler(
    IOrderRepository orderRepository,
    IRouteTripRepository routeTripRepository,
    IUnitOfWork unitOfWork,
    ILogger<AcceptOrderCommandHandler> logger)
    : ICommandHandler<AcceptOrderCommand>
{
    public async Task<Result> Handle(AcceptOrderCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Processing accept order command for order {OrderId}",
            request.OrderId);

        try
        {
            var orderHeader = await orderRepository.GetAsync(request.OrderId, cancellationToken);

            if (orderHeader is null)
            {
                logger.LogError(
                    "Order {OrderId} not found while processing accept order command",
                    request.OrderId);
                return Result.Failure(OrderErrors.NotFound(request.OrderId));
            }

            logger.LogInformation(
                "Found order {OrderId} in status {Status}. Validating route trip assignment.",
                orderHeader.Id,
                orderHeader.Status);

            if (orderHeader.RouteTripId is null)
            {
                logger.LogError(
                    "Order {OrderId} has no route trip assigned. Cannot accept order.",
                    request.OrderId);
                return Result.Failure(OrderErrors.NoRouteTripHasAssigned(request.OrderId));
            }

            logger.LogInformation(
                "Validating route trip {RouteTripId} for order {OrderId}",
                orderHeader.RouteTripId,
                request.OrderId);

            var routeTrip = await routeTripRepository.GetAsync((Guid)orderHeader.RouteTripId, cancellationToken);

            if (routeTrip is null)
            {
                logger.LogError(
                    "Route trip {RouteTripId} not found for order {OrderId}",
                    orderHeader.RouteTripId,
                    request.OrderId);
                return Result.Failure(RouteTripErrors.NotFound((Guid)orderHeader.RouteTripId));
            }

            if (routeTrip.Status is not RouteTripStatus.Approved)
            {
                logger.LogInformation("Route trip {RouteTripId} has no status is not yet approved.", orderHeader.RouteTripId);
                return Result.Failure(RouteTripErrors.InvalidStatus(RouteTripStatus.Approved));
            }

            logger.LogInformation(
                "Route trip {RouteTripId} validated. Accepting order {OrderId}",
                routeTrip.Id,
                request.OrderId);

            var result = orderHeader.Accept();

            if (result.IsFailure)
            {
                logger.LogError(
                    "Failed to accept order {OrderId}. Error: {Error}",
                    request.OrderId,
                    result.Error);
                return result;
            }

            logger.LogInformation(
                "Order {OrderId} accepted successfully. Saving changes.",
                request.OrderId);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            logger.LogInformation(
                "Order {OrderId} acceptance completed. Status changed to {Status}",
                request.OrderId,
                orderHeader.Status);

            return Result.Success();
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Unexpected error while accepting order {OrderId}",
                request.OrderId);
            return Result.Failure(OrderErrors.Unknown(request.OrderId));
        }
    }
}
