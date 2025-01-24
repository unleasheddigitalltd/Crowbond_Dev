using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Routes.GetRoutes;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Routes;

internal sealed class GetRoutes : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("routes", async (
            ISender sender,
            string search = "",
            string sort = "name",
            string order = "asc",
            int page = 0,
            int size = 10) =>
        {
            Result<RoutesResponse> result = await sender.Send(new GetRoutesQuery(search, sort, order, page, size));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetRoutes)
            .WithTags(Tags.Routes);
    }
}
