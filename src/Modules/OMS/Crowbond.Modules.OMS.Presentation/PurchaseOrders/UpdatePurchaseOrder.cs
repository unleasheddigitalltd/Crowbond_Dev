using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.PurchaseOrders.UpdatePurchaseOrder;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.PurchaseOrders;

internal sealed class UpdatePurchaseOrder : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("purchaseorders/{id}", async (Guid id, PurchaseOrderDto request, ISender sender) =>
        {
            Result result = await sender.Send(new UpdatePurchaseOrderCommand(id, request));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .RequireAuthorization(Permission.ModifyPurchaseOrders)
        .WithTags(Tags.PurchaseOrders);
    }
}
