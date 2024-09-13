using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Abstractions.Authentication;
using Crowbond.Modules.WMS.Application.Tasks.PutAway.GetPutAwayTaskAssignmentLines;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Tasks.PutAway;

internal sealed class GetPutAwayTaskAssignmentLines : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("tasks/putaway/{taskId}/lines", async (IWarehouseOperatorContext operatorContext, Guid taskId, ISender sender) =>
        {
            Result<IReadOnlyCollection<TaskAssignmentLineResponse>> result = await sender.Send(
                new GetPutAwayTaskAssignmentLinesQuery(operatorContext.WarehouseOperatorId, taskId));

            return result.Match(Results.Ok, ApiResults.Problem);
        }
        )
            .RequireAuthorization(Permissions.ExecutePutAwayTasks)
            .WithTags(Tags.PutAway);
    }
}
