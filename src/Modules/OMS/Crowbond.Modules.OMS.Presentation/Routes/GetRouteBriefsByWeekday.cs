using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.Routes.GetRouteBriefsByWeekday;
using Crowbond.Modules.OMS.Domain.Routes;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.Routes;

public sealed class GetRouteBriefsByWeekday : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("routes/briefs/{weekday}", async (Weekday weekday, ISender sender) =>
        {
            Result<IReadOnlyCollection<RouteResponse>> result = await sender.Send(new GetRouteBriefsByWeekdayQuery(weekday));

            return result.Match(Results.Ok, ApiResults.Problem);
        }).RequireAuthorization(Permissions.GetRoutes)
        .WithTags(Tags.Routes);
    }
}
