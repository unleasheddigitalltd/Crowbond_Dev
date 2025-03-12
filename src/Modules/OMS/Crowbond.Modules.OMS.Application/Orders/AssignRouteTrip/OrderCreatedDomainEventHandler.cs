using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.CRM.PublicApi;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Domain.RouteTrips;

namespace Crowbond.Modules.OMS.Application.Orders.AssignRouteTrip;

internal sealed class OrderCreatedDomainEventHandler(
    ICustomerApi customerApi,
    IOrderRepository orderRepository,
    IRouteTripRepository routeTripRepository,
    IUnitOfWork unitOfWork) : DomainEventHandler<OrderCreatedDomainEvent>
{
    public override async Task Handle(
        OrderCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        var header = await orderRepository.GetAsync(domainEvent.OrderId, cancellationToken);
        if (header is null)
        {
            throw new Exception(OrderErrors.NotFound(domainEvent.OrderId).ToString());
        }

        // Get the customer outlet details
        var outlet = await customerApi.GetOutletForOrderAsync(header.CustomerOutletId, cancellationToken);
        if (outlet is null)
        {
            throw new Exception($"Customer outlet not found: {header.CustomerOutletId}");
        }

        // Get the route for this weekday
        var weekday = header.ShippingDate.DayOfWeek;
        var outletRoute = await customerApi.GetOutletRouteForDayAsync(
            header.CustomerOutletId,
            weekday,
            cancellationToken);

        if (outletRoute is null)
        {
            // No route assigned for this delivery location on this day
            // Just return so order stays in 'Pending'
            return;
        }

        // Get or create route trip for this date
        var routeTrip = await routeTripRepository.GetByDateAndRouteAsync(
            header.ShippingDate,
            outletRoute.RouteId,
            cancellationToken);

        if (routeTrip is null)
        {
            // Create new route trip
            routeTrip = RouteTrip.Create(
                header.ShippingDate,
                outletRoute.RouteId);

            routeTripRepository.Insert(routeTrip);
        }

        // Assign the route trip to the order
        var result = header.AssignRouteTrip(routeTrip.Id, outletRoute.RouteName);
        if (result.IsFailure)
        {
            // Just return so order stays in 'Pending'
            return;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
