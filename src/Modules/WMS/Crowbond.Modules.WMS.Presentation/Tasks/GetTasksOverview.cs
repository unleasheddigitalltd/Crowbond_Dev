using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Tasks.GetTasksOverview;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Tasks;

internal sealed class GetTasksOverview : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("tasks/overview", async (
            string? sort,
            string? order,
            int? page,
            int? pageSize,
            ISender sender) =>
        {
            var result = await sender.Send(
                new GetTasksOverviewQuery(
                    sort,
                    order,
                    page ?? 1,
                    pageSize ?? 10));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.ViewTasks)
        .WithTags(Tags.Tasks);
    }
}
