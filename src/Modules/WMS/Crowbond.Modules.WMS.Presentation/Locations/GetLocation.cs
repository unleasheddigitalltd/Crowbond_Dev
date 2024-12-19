using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Locations.GetLocation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Locations;

internal sealed class GetLocation : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("locations/{id}", async (Guid id, ISender sender) =>
        {
            Result<LocationResponse> result = await sender.Send(new GetLocationQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetLocations)
            .WithTags(Tags.Locations);
    }
}
