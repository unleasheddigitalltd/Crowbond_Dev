using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Tasks.PutAway.GetPutAwayUnassignedTasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Tasks.PutAway;

public sealed class GetPutAwayUnassignedTasks : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("tasks/putaway/unassigned", async (
            ISender sender,
            string search = "",
            string sort = "status",
            string order = "asc",
            int page = 0,
            int size = 10) =>
        {
            Result<PutAwayTasksResponse> result = await sender.Send(new GetPutAwayUnassignedTasksQuery(search, sort, order, page, size));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.GetPutAwayTasks)
            .WithTags(Tags.PutAway);
    }
}
