using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.RouteTrips.GetRouteTripsByDate;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.RouteTrips;

internal sealed class GetRouteTripsByDate : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("routes/trips/{date}", async (DateOnly date, ISender sender) =>
        {
            Result<IReadOnlyCollection<RouteTripResponse>> result = await sender.Send(new GetRouteTripsByDateQuery(date));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetRouteTrips)
            .WithTags(Tags.Routes);
    }
}
