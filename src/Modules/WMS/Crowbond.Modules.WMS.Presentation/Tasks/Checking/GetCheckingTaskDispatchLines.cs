using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Tasks.Checking.GetCheckingTaskDispatchLines;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Tasks.Checking;

internal sealed class GetCheckingTaskDispatchLines : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("tasks/checking/{id}/dispatch-lines", async (Guid id, ISender sender) =>
        {
            Result<IReadOnlyCollection<TaskDispatchLineResponse>> result = await sender.Send(new GetCheckingTaskDispatchLinesQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ExecuteCheckingTasks)
            .WithTags(Tags.Checking);
    }
}
