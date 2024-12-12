using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Abstractions.Authentication;
using Crowbond.Modules.WMS.Application.Tasks.PutAway.LocateProductForPutAway;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Tasks.PutAway;

internal sealed class LocateProductForPutAway : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("tasks/putaway/{id}/locate", async (IWarehouseOperatorContext operatorContext, Guid id, Request request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new LocateProductForPutAwayCommand(
                operatorContext.WarehouseOperatorId,
                id,
                request.ReceiptLineId,
                request.LocationId,
                request.Qty));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ExecutePutAwayTasks)
            .WithTags(Tags.PutAway);
    }

    private sealed record Request(Guid ReceiptLineId, Guid LocationId, decimal Qty);
}
