using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Abstractions.Authentication;
using Crowbond.Modules.WMS.Application.Tasks.Checking.UpdateCheckingTask;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Tasks.Checking;

internal sealed class UpdateCheckingTask : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("tasks/checking/{id}", async (Guid id, IWarehouseOperatorContext operatorContext, List<CheckingDispatchLineRequest> requests, ISender sender) =>
        {
            Result result = await sender.Send(new UpdateCheckingTaskCommand(
                operatorContext.WarehouseOperatorId,
                id,
                requests));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ExecuteCheckingTasks)
            .WithTags(Tags.Checking);
    }    
}
