using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Locations.HoldLocation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Locations;

internal sealed class HoldLocation : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("locations/{id}/Hold", async (Guid id, ISender sender) =>
        {
            Result result = await sender.Send(new HoldLocationCommand(id));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ModifyLocations)
            .WithTags(Tags.Locations);
    }
}
