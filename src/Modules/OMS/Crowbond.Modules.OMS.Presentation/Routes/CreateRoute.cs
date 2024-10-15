using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Routes.CreateRoute;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Routes;

internal sealed class CreateRoute : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("routes", async (Request request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreateRouteCommand(
                request.Name,
                request.Position,
                request.CutOffTime,
                request.DaysOfWeek));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.CreateRoutes)
            .WithTags(Tags.Routes);
    }

    private sealed record Request(string Name, int Position, TimeOnly CutOffTime, string DaysOfWeek);
}
