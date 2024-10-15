using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Routes.UpdateRoute;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Routes;

internal sealed class UpdateRoute : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("routes/{id}", async (Guid id, Request request, ISender sender) =>
        {
            Result result = await sender.Send(new UpdateRouteCommand(
                id,
                request.Name,
                request.Position,
                request.CutOffTime,
                request.DaysOfWeek));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ModifyRoutes)
            .WithTags(Tags.Routes);
    }

    private sealed record Request(string Name, int Position, TimeOnly CutOffTime, string DaysOfWeek);
}
