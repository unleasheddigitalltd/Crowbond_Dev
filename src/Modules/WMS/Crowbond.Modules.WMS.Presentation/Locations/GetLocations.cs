﻿using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Locations.GetLocations;
using Crowbond.Modules.WMS.Domain.Locations;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Locations;

internal sealed class GetLocations : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("locations", async (LocationLayer locationLayer, LocationType locationType, ISender sender) =>
        {
            Result<IReadOnlyCollection<LocationResponse>> result = await sender.Send(new GetLocationsQuery(locationLayer, locationType));
            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetLocations)
            .WithTags(Tags.Locations);
    }
}
