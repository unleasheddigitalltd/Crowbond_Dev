using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Locations.GetLocations;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Locations;

internal sealed class GetLocations : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("locations", async (ISender sender,
            string search = "",
            string sort = "name",
            string order = "asc",
            int page = 0,
            int size = 10
            ) =>
        {
            Result<LocationsResponse> result = await sender.Send(
                new GetLocationsQuery(search, sort, order, page, size));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetLocations)
        .WithTags(Tags.Locations);
    }
}
