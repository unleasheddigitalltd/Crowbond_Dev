using System.Security.Claims;
using Crowbond.Common.Domain;
using Crowbond.Common.Infrastructure.Authentication;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.RouteTrips.LogOnRouteTrip;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.RouteTrips;

internal sealed class LogOnRouteTrip : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("routes/trips/log-on", async (Request request, ClaimsPrincipal claims, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new LogOnRouteTripCommand(
                request.RouteTripId, claims.GetUserId()));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.ModifyRouteTriplogs)
        .WithTags(Tags.Routes);
    }

    internal sealed class Request
    {
        public Guid RouteTripId { get; init; }
    }
}
