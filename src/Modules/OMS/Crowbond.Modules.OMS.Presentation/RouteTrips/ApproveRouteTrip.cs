using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.RouteTrips.ApproveRouteTrip;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.RouteTrips;

internal sealed class ApproveRouteTrip : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("routes/trips/{id}/approve", async (Guid id, ISender sender) =>
        {
            Result result = await sender.Send(new ApproveRouteTripCommand(id));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ApproveRouteTrip)
            .WithTags(Tags.Routes);
    }
}
