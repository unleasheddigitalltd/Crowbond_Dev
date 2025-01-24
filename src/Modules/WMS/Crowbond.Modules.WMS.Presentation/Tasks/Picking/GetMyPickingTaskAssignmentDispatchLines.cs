using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Tasks.Picking.GetMyPickingTaskAssignmentLines;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Tasks.Picking;

internal sealed class GetMyPickingTaskAssignmentDispatchLines : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("tasks/picking/{id}/assignments/dispatch-lines", async (Guid id, ISender sender) =>
        {
            Result<IReadOnlyCollection<DispatchLineResponse>> result = await sender.Send(
                new GetMyPickingTaskAssignmentDispatchLinesQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        }
        )
            .RequireAuthorization(Permissions.ExecutePickingTasks)
            .WithTags(Tags.Picking);
    }
}
