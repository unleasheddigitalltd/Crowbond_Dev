using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Tasks.PutAway.GetPutAwayTasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Tasks.PutAway;

internal sealed class GetPutawayTasks : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("tasks/putaway", async (
            ISender sender,
            string search = "",
            string sort = "receiveddate",
            string order = "decs",
            int page = 0,
            int size = 10) =>
        {
            Result<PutAwayTasksResponse> result = await sender.Send(new GetPutAwayTasksQuery(search, sort, order, page, size));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetPutAwayTasks)
            .WithTags(Tags.PutAway);
    }
}
