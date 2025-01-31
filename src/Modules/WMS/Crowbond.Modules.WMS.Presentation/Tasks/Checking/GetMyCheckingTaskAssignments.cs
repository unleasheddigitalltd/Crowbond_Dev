using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Abstractions.Authentication;
using Crowbond.Modules.WMS.Application.Tasks.Checking.GetMyCheckingTaskAssignments;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Tasks.Checking;

internal sealed class GetMyCheckingTaskAssignments : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("tasks/checking/my/assignments", async (IWarehouseOperatorContext operatorContext, ISender sender) =>
        {
            Result<IReadOnlyCollection<TaskAssignmentResponse>> result = await sender.Send(
                new GetMyCheckingTaskAssignmentsQuery(operatorContext.WarehouseOperatorId));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ExecuteCheckingTasks)
            .WithTags(Tags.Checking);
    }
}
