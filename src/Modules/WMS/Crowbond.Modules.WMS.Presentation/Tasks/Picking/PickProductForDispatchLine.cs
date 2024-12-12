using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.WMS.Application.Abstractions.Authentication;
using Crowbond.Modules.WMS.Application.Tasks.Picking.PickProductForDispatchLine;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.WMS.Presentation.Tasks.Picking;

internal sealed class PickProductForDispatchLine : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("tasks/picking/{id}/pick", async (Guid id, IWarehouseOperatorContext warehouseOperatorContext, Request request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new PickProductForDispatchLineCommand(
                id,
                warehouseOperatorContext.WarehouseOperatorId,
                request.DispatchLineId,
                request.StockId,
                request.ToLocationId,
                request.Qty));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ExecutePickingTasks)
            .WithTags(Tags.Picking);
    }

    private sealed record Request(Guid DispatchLineId, decimal Qty, Guid StockId, Guid ToLocationId);
}
