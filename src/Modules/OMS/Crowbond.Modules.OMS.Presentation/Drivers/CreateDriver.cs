using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Drivers.CreateDriver;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Drivers;

internal sealed class CreateDriver : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("drivers", async (DriverRequest request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreateDriverCommand(Guid.NewGuid(), request));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.CreateDrivers)
        .WithTags(Tags.Drivers);
    }
}
