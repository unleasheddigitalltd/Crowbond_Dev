using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.RouteTrips.GetRouteTripsByStatus;
using Crowbond.Modules.OMS.Application.RouteTrips.GetRouteTripsWithPendingOrders;
using Crowbond.Modules.OMS.Domain.RouteTrips;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.OrdersRouteTrips;

public class RouteTripsAndOrders : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("route/trips/orders/pending", async (ISender sender) =>
        {
            // Query for route trips that have orders with Pending status
            var result = await sender.Send(new GetRouteTripsWithPendingOrdersQuery());
            return result.Match(() => Results.Ok(result), ApiResults.Problem);
        }).RequireAuthorization().WithTags(Tags.Routes);
    }
}
