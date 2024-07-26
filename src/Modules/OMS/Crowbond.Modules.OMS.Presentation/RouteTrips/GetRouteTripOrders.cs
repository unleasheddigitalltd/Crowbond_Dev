using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.RouteTrips.GetRouteTripOrders;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.RouteTrips;

internal sealed class GetRouteTripOrders : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("routes/trips/{id}/orders", async (Guid id, ISender sender) =>
        {
            Result<IReadOnlyCollection<OrderResponse>> reault = await sender.Send(new GetRouteTripOrdersQuery(id));

            return reault.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetRouteTrips)
            .WithTags(Tags.Routes);
    }
}
