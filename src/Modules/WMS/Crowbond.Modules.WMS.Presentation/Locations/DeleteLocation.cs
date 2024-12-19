using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Locations.DeleteLocation;
using MassTransit.Initializers.PropertyProviders;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Locations;

internal sealed class DeleteLocation : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("locations/{id}", async (Guid id, ISender sender) =>
        {
            Result result = await sender.Send(new DeleteLocationCommand(id));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.DeleteLocations)
            .WithTags(Tags.Locations);
    }
}
