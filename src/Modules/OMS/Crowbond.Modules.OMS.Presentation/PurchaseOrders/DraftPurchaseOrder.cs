using Crowbond.Common.Domain;
using Crowbond.Common.Presentation.Endpoints;
using Crowbond.Common.Presentation.Results;
using Crowbond.Modules.OMS.Application.PurchaseOrders.DraftPurchaseOrder;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Crowbond.Modules.OMS.Presentation.PurchaseOrders;

internal sealed class DraftPurchaseOrder : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("purchase-orders/{id}/draft", async (Guid id, ISender sender) =>
        {
            Result result = await sender.Send(new DraftPurchaseOrderCommand(id));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ModifyPurchaseOrders)
            .WithTags(Tags.PurchaseOrders);
    }
}
