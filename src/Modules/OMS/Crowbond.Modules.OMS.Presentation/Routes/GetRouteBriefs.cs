using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Routes.GetRoutes;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Routes;

internal sealed class GetRouteBriefs : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("routes/briefs", async (ISender sender) =>
        {
            Result<IReadOnlyCollection<RouteResponse>> result = await sender.Send(new GetRouteBriefsQuery());

            return result.Match(Results.Ok, ApiResults.Problem);
        }).RequireAuthorization(Permissions.GetRoutes)
        .WithTags(Tags.Routes);
        
    }
}
