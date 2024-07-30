using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.RouteTrips.CreateRouteTrip;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.RouteTrips;

internal sealed class CreateRouteTrip : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("routes/trips", async (RouteTripRequest request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreateRouteTripCommand(request));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.CreateRouteTrips)
            .WithTags(Tags.Routes);
    }
}
