using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Crowbond.Modules.OMS.Application.PurchaseOrders.PendPurchaseOrder;

namespace Crowbond.Modules.OMS.Presentation.PurchaseOrders;

internal sealed class PendPurchaseOrder : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("purchase-orders/{id}/pend", async (Guid id, ISender sender) =>
        {
            Result result = await sender.Send(new PendPurchaseOrderCommand(id));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ModifyPurchaseOrders)
            .WithTags(Tags.PurchaseOrders);
    }
}
