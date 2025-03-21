using System.Globalization;
using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.CRM.PublicApi;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Domain.RouteTrips;
using Microsoft.Extensions.Logging;

namespace Crowbond.Modules.OMS.Application.Orders.AssignRouteTrip;

internal sealed class OrderCreatedDomainEventHandler(
    ICustomerApi customerApi,
    IOrderRepository orderRepository,
    IRouteTripRepository routeTripRepository,
    IUnitOfWork unitOfWork,
    ILogger<OrderCreatedDomainEventHandler> logger) : DomainEventHandler<OrderCreatedDomainEvent>
{
    public override async Task Handle(
        OrderCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Processing order created event for order {OrderId}",
            domainEvent.OrderId);

        try
        {
            var header = await orderRepository.GetAsync(domainEvent.OrderId, cancellationToken);
            if (header is null)
            {
                logger.LogError(
                    "Order {OrderId} not found while processing OrderCreatedDomainEvent",
                    domainEvent.OrderId);
                throw new Exception(OrderErrors.NotFound(domainEvent.OrderId).ToString());
            }

            logger.LogInformation(
                "Looking up customer outlet {OutletId} for order {OrderId}",
                header.CustomerOutletId,
                domainEvent.OrderId);

            // Get the customer outlet details
            var outlet = await customerApi.GetOutletForOrderAsync(header.CustomerOutletId, cancellationToken);
            if (outlet is null)
            {
                logger.LogError(
                    "Customer outlet {OutletId} not found for order {OrderId}",
                    header.CustomerOutletId,
                    domainEvent.OrderId);
                throw new Exception($"Customer outlet not found: {header.CustomerOutletId}");
            }

            // Get the route for this weekday
            var weekday = header.ShippingDate.DayOfWeek;
            logger.LogInformation(
                "Looking up route for outlet {OutletId} on {Weekday} for shipping date {ShippingDate}",
                header.CustomerOutletId,
                weekday,
                header.ShippingDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));

            var outletRoute = await customerApi.GetOutletRouteForDayAsync(
                header.CustomerOutletId,
                weekday,
                cancellationToken);

            if (outletRoute is null)
            {
                logger.LogWarning(
                    "No route found for outlet {OutletId} on {Weekday}. Order {OrderId} will remain in Pending status",
                    header.CustomerOutletId,
                    weekday,
                    domainEvent.OrderId);
                return;
            }

            logger.LogInformation(
                "Found route {RouteName} for outlet {OutletId}. Looking up route trip for date {ShippingDate}",
                outletRoute.RouteName,
                header.CustomerOutletId,
                header.ShippingDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));

            // Get or create route trip for this date
            var routeTrip = await routeTripRepository.GetByDateAndRouteAsync(
                header.ShippingDate,
                outletRoute.RouteId,
                cancellationToken);

            if (routeTrip is null)
            {
                logger.LogInformation(
                    "Creating new route trip for route {RouteName} on date {ShippingDate}",
                    outletRoute.RouteName,
                    header.ShippingDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));

                routeTrip = RouteTrip.Create(
                    header.ShippingDate,
                    outletRoute.RouteId);

                routeTripRepository.Insert(routeTrip);
            }

            logger.LogInformation(
                "Assigning route trip {RouteTripId} to order {OrderId}",
                routeTrip.Id,
                domainEvent.OrderId);

            // Assign the route trip to the order
            var result = header.AssignRouteTrip(routeTrip.Id, outletRoute.RouteName);
            if (result.IsFailure)
            {
                logger.LogError(
                    "Failed to assign route trip {RouteTripId} to order {OrderId}. Error: {Error}",
                    routeTrip.Id,
                    domainEvent.OrderId,
                    result.Error);
                return;
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            logger.LogInformation(
                "Successfully assigned route trip {RouteTripId} to order {OrderId}",
                routeTrip.Id,
                domainEvent.OrderId);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Unexpected error while processing order created event for order {OrderId}",
                domainEvent.OrderId);
        }
    }
}
