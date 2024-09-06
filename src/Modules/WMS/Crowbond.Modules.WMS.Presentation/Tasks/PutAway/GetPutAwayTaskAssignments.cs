using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Abstractions.Authentication;
using Crowbond.Modules.WMS.Application.Tasks.PutAway.GetPutAwayTaskAssignments;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Tasks.PutAway;

internal sealed class GetPutAwayTaskAssignments : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("tasks/putaway/assignments", async (IWarehouseOperatorContext operatorContext, ISender sender) =>
        {
            Result<IReadOnlyCollection<TaskAssignmentResponse>> result = await sender.Send(
                new GetPutAwayTaskAssignmentsQuery(operatorContext.WarehouseOperatorId));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ExecutePutAwayTasks)
            .WithTags(Tags.PutAway);
    }
}
