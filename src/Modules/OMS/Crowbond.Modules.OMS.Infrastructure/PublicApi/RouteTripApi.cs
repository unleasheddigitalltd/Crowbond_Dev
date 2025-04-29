using System.Globalization;
using Crowbond.Modules.CRM.PublicApi;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Domain.RouteTrips;
using Crowbond.Modules.OMS.PublicApi;
using Microsoft.Extensions.Logging;

namespace Crowbond.Modules.OMS.Infrastructure.PublicApi;

internal sealed class RouteTripApi(
    ICustomerApi customerApi, IOrderRepository orderRepository, IRouteTripRepository routeTripRepository,
    IUnitOfWork unitOfWork, ILogger<RouteTripApi> logger
    ) : IRouteTripApi
{
    public async Task AssignRouteTripToOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
          logger.LogInformation(
            "AssignRouteTripToOrderAsync for order {OrderId}",
            orderId);

        try
        {
            // get the order header
            var header = await orderRepository.GetAsync(orderId, cancellationToken);
            if (header is null)
            {
                logger.LogError(
                    "Order {OrderId} not found while processing AssignRouteTripToOrderAsync",
                    orderId);
                throw new Exception(OrderErrors.NotFound(orderId).ToString());
            }

            logger.LogInformation(
                "Looking up customer outlet {OutletId} for order {OrderId}",
                header.CustomerOutletId,
                orderId);

            // Get the customer outlet details
            var outlet = await customerApi.GetOutletForOrderAsync(header.CustomerOutletId, cancellationToken);
            if (outlet is null)
            {
                logger.LogError(
                    "Customer outlet {OutletId} not found for order {OrderId}",
                    header.CustomerOutletId,
                    orderId);
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
                    orderId);
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
                orderId);

            // Assign the route trip to the order
            var result = header.AssignRouteTrip(routeTrip.Id, outletRoute.RouteName);
            if (result.IsFailure)
            {
                logger.LogError(
                    "Failed to assign route trip {RouteTripId} to order {OrderId}. Error: {Error}",
                    routeTrip.Id,
                    orderId,
                    result.Error);
                return;
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            logger.LogInformation(
                "Successfully assigned route trip {RouteTripId} to order {OrderId}",
                routeTrip.Id,
                orderId);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Unexpected error while assigning route trip for order {OrderId}",
                orderId);
        }
    }
}
