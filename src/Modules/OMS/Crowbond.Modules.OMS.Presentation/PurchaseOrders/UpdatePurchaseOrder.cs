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
        app.MapPut("purchase-orders/{id}", async (Guid id, PurchaseOrderRequest request, ISender sender) =>
        {
            Result result = await sender.Send(new UpdatePurchaseOrderCommand(id, request));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
            .RequireAuthorization(Permissions.ModifyPurchaseOrders)
            .WithTags(Tags.PurchaseOrders);
    }
}
