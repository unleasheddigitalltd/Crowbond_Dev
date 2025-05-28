using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Abstractions.Authentication;
using Crowbond.Modules.WMS.Application.Tasks.Picking.CompletePickingLine;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Tasks.Picking;

internal sealed class CompletePickingLine : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("tasks/picking/{id}/picking-lines/{pickingLineId}/complete", async (
            IWarehouseOperatorContext operatorContext,
            Guid id,
            Guid pickingLineId,
            CompletePickingLineRequest request,
            ISender sender) =>
        {
            Result result = await sender.Send(new CompletePickingLineCommand(
                operatorContext.WarehouseOperatorId, 
                id, 
                pickingLineId, 
                request.CompletedQty));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ExecutePickingTasks)
            .WithTags(Tags.Picking);
    }
}

public class CompletePickingLineRequest
{
    public decimal CompletedQty { get; set; }
}
