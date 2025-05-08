using System.Xml.Linq;
using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Locations.CreateLocation;
using Crowbond.Modules.WMS.Domain.Locations;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Locations;

internal sealed class CreateLocation : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("locations", async (Request request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreateLocationCommand(
                request.ParentId,
                request.Name,
                request.ScanCode,
                request.NetworkAddress,
                request.PrinterName,
                request.LocationType,
                request.LocationLayer));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.CreateLocations)
            .WithTags(Tags.Locations);
    }

    private sealed record Request(
        Guid? ParentId,
        string Name,
        string? ScanCode,
        string? NetworkAddress,
        string? PrinterName,
        LocationType? LocationType,
        LocationLayer LocationLayer);
}
