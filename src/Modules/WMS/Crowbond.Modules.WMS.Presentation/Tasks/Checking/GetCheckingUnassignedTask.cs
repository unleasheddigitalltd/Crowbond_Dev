using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Tasks.Checking.GetCheckingUnassignedTask;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Tasks.Checking;

internal sealed class GetCheckingUnassignedTask : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("tasks/checking/unassigned", async (
            ISender sender,
            string search = "",
            string sort = "taskNo",
            string order = "asc",
            int page = 0,
            int size = 10) =>
        {
            Result<CheckingTasksResponse> result = await sender.Send(
                new GetCheckingTasksUnassignedQuery(search, sort, order, page, size));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ExecuteCheckingTasks)
            .WithTags(Tags.Checking);
    }
}
