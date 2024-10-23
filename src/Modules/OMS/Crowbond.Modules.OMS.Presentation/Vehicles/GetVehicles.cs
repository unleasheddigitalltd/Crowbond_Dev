using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Vehicles.GetVehicles;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Vehicles;

internal sealed class GetVehicles : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("vehicles", async (
            ISender sender,
            string search = "",
            string sort = "name",
            string order = "asc",
            int page = 0,
            int size = 10
            ) =>
        {
            Result<VehiclesResponse> result = await sender.Send(
                new GetVehiclesQuery(search, sort, order, page, size));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetVehicles)
            .WithTags(Tags.Vehicles);
    }
}
